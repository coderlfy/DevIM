/****************************************
***创建人：bhlfy
***创建时间：2013-11-12 09:00:29
***公司：ICat科技
***文件描述：注册信息内容管理器。
*****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public class RegisterInformation
    {
        /// <summary>
        /// 序列号组成结构
        /// </summary>
        public const string SerialNumberFormat = "{0};{1}";
        /// <summary>
        /// 软件注册路径根节点
        /// </summary>
        public const string RootNodeRegisterPath = @"SOFTWARE\BoHua\MineSystemNetworking\1.0";
        /// <summary>
        /// 用户名的键名
        /// </summary>
        public const string KeyNameUsername = "Username";
        /// <summary>
        /// 磁盘序列号的键名
        /// </summary>
        public const string KeyNameDiskNumber = "DiskNumber";
        /// <summary>
        /// 系统注册码的键名
        /// </summary>
        public const string KeyNameSerialNumber = "SerialNumber";
        /// <summary>
        /// 注册用户名
        /// </summary>
        public string Username = "";
        /// <summary>
        /// 磁盘序列号
        /// </summary>
        public string DiskNumber = "";
        /// <summary>
        /// 系统注册码
        /// </summary>
        public string SerialNumber = "";
    }
}
