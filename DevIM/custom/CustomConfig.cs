using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DevIM.custom
{
    public class CustomConfig
    {
        private const String ServiceURLConfigName = "ServiceURL";
        private const String LogDirectoryKeyName = "LogDirectoryName";
        private const String ApplicationNameKey = "ApplicationName";
        private const String ApplicationVersionKeyName = "ApplicationVersion";
        private const String TextBoxMaxLineKeyName = "TextBoxMaxLine";
        public const String IconStreamName = "CenterServices.App.ico";
        public const String MiddleDBKeyName = "ConnectStringMiddle";
        public const String EnableAutoStartServiceKeyName = "EnableAutoStartService";

        private static object _defaultServiceURL = "net.tcp://localhost:22222";
        private static object _logDirName = "logs";
        private static String _appName = "DevIM";
        private static object _EnableAutoStartService = false;

        public const String DevCompanyName = "山西ICat科技有限公司";
        public const String Developer = "bhlfy";
        public const String HelpTelephone = "0351-7037628";
        public const String DevStartDate = "2013-10-20";
        public const String AboutSoftware = @"该软件定位于。";

        private static object _textBoxMaxLine = 1000;
        /// <summary>
        /// 中心端提供的接口服务地址
        /// </summary>
        public static object ServiceURL
        {
            #region
            get
            {
                return _defaultServiceURL;
            }
            set
            {
                _defaultServiceURL = value;
            }
            #endregion
        }
        /// <summary>
        /// 是否自启动服务
        /// </summary>
        public static object EnableAutoStartService
        {
            #region
            get
            {
                return _EnableAutoStartService;
            }
            set
            {
                _EnableAutoStartService = value;
            }
            #endregion
        }
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public static String ApplicationName
        {
            #region
            get
            {
                return _appName;
            }
            #endregion
        }
        /// <summary>
        /// 应用程序版本
        /// </summary>
        public static String ApplicationVersion
        {
            #region
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            #endregion
        }
        /// <summary>
        /// 应用程序日志文件名
        /// </summary>
        public static object LogDirectoryName
        {
            #region
            get
            {
                return _logDirName;
            }
            set
            {
                _logDirName = value;
            }
            #endregion
        }
        /// <summary>
        /// 控制台最大显示行数
        /// </summary>
        public static object TextBoxMaxLine
        {
            #region
            get
            {
                return _textBoxMaxLine;
            }
            set
            {
                _textBoxMaxLine = value;
            }
            #endregion
        }
        /// <summary>
        /// 获取系统参数
        /// </summary>
        public static void GetSystemParameters()
        {
            #region
            Config.Get(CustomConfig.ServiceURLConfigName, ref _defaultServiceURL);

            Config.Get(CustomConfig.LogDirectoryKeyName, ref _logDirName);

            Config.Get(CustomConfig.TextBoxMaxLineKeyName, ref _textBoxMaxLine);

            Config.Get(CustomConfig.EnableAutoStartServiceKeyName ,ref _EnableAutoStartService);

            //MiddleDBConnectionString = Config.GetConnectString(MiddleDBKeyName);
            #endregion
        }
    }
}
