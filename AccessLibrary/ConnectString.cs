/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 01:47:25
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    class ConnectString
    {
        private static string _defaultConnKeyName = "ConnectString";
        /// <summary>
        /// 获取默认的数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string getDefault()
        {
            #region
            return getConfig(_defaultConnKeyName);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getConfig(string key)
        {
            #region
            ConnectionStringSettings configsetting = ConfigurationManager.ConnectionStrings[key];
            if (configsetting == null)
                Fundation.Core.ExtConsole.Write("读取该应用程序的配置文件（config）的connectionStrings区时发生错误！");

            string connectstring = ConfigurationManager.ConnectionStrings[key].ConnectionString;

            if (string.IsNullOrEmpty(connectstring))
                Fundation.Core.ExtConsole.Write(string.Format("读取该应用程序的配置文件（config）的{0}属性时发生错误！", key));

            return connectstring;
            #endregion
        }
    }
}
