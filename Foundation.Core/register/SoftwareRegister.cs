/****************************************
***创建人：bhlfy
***创建时间：2013-11-12 09:00:29
***公司：ICat科技
***文件描述：软件注册器（实现软件注册，注册信息的读取，验证等功能）。
*****************************************/
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace Fundation.Core
{
    public class SoftwareRegister
    {
        /// <summary>
        /// 返回信息
        /// </summary>
        public static string ErrorInformation = "";
        /// <summary>
        /// 注册信息
        /// </summary>
        public static RegisterInformation _RegisterInfor = null;
        /// <summary>
        /// 读取注册信息
        /// </summary>
        /// <returns></returns>
        private static bool readRegisterInfor()
        {
            #region
            RegistryKey regRootKey = Registry.CurrentUser;
            RegistryKey regSubKey;
            try
            {
                regSubKey = regRootKey.OpenSubKey(RegisterInformation.RootNodeRegisterPath);

                _RegisterInfor = new RegisterInformation();
                _RegisterInfor.SerialNumber = regSubKey
                    .GetValue(RegisterInformation.KeyNameSerialNumber).ToString();

                _RegisterInfor.Username = regSubKey
                    .GetValue(RegisterInformation.KeyNameUsername).ToString();

                _RegisterInfor.DiskNumber = regSubKey
                    .GetValue(RegisterInformation.KeyNameDiskNumber).ToString();
                return true;
            }
            catch (Exception e)
            {
                //预期异常类型应该两种
                //1.没有本公司的注册信息。
                //2.注册信息不全。
                //这两种情况需重新注册
                ErrorInformation = e.ToString();
                return false;
            }
            #endregion
        }
        /// <summary>
        /// 读取到的注册信息验证有效性
        /// </summary>
        /// <returns></returns>
        public static bool IsValid()
        {
            #region
            bool isvalid = false;

            if (readRegisterInfor())
                isvalid = utiIsValid();

            return isvalid;
            #endregion
        }
        /// <summary>
        /// 验证注册信息的有效性（复用）
        /// </summary>
        /// <returns></returns>
        private static bool utiIsValid()
        {
            #region
            bool isvalid = false;
            string[] serialparams = new string[2];
            string tempserialnumber = "";
            serialparams[0] = _RegisterInfor.Username;
            serialparams[1] = _RegisterInfor.DiskNumber;
            tempserialnumber = string.Format(
                RegisterInformation.SerialNumberFormat, serialparams);

            if (Encrypt.EncryptString(tempserialnumber)
                == _RegisterInfor.SerialNumber)
                if (_RegisterInfor.DiskNumber == HardDisk.GetSerialNumber())
                    isvalid = true;
                else
                {
                    ErrorInformation = "请注重版权，重新注册！";
                    isvalid = false;
                }
            else
            {
                isvalid = false;
                ErrorInformation = "注册信息错误，请重新注册！";
            }
            return isvalid;
            #endregion
        }
        /// <summary>
        /// 判定正在注册的有效性
        /// </summary>
        /// <returns></returns>
        private static bool IsRegisteringValid()
        {
            #region
            return utiIsValid();
            #endregion
        }
        /// <summary>
        /// 软件注册开始执行
        /// </summary>
        /// <param name="willreginfor">即将注册的信息</param>
        /// <returns>注册是否成功</returns>
        public static bool Start(RegisterInformation willreginfor)
        {
            #region
            _RegisterInfor = willreginfor;

            if (IsRegisteringValid())
            {
                RegistryKey regRootKey = Registry.CurrentUser;

                RegistryKey regSubKey = regRootKey.CreateSubKey(
                    RegisterInformation.RootNodeRegisterPath, RegistryKeyPermissionCheck.ReadWriteSubTree);

                regSubKey.SetValue(RegisterInformation.KeyNameSerialNumber, _RegisterInfor.SerialNumber,
                    RegistryValueKind.String);

                regSubKey.SetValue(RegisterInformation.KeyNameUsername, _RegisterInfor.Username,
                    RegistryValueKind.String);

                regSubKey.SetValue(RegisterInformation.KeyNameDiskNumber, _RegisterInfor.DiskNumber,
                    RegistryValueKind.String);

                regRootKey.Close();
                return true;
            }
            return false;
            #endregion
        }
    }
}
