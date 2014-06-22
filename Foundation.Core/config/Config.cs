/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public class Config
    {
        private static Hashtable _configCacheGroup
            = new Hashtable();
        /// <summary>
        /// 并发写操作的“锁”
        /// </summary>
        private static readonly object _lockConfig = true;

        private static Configuration _configuration = ConfigurationManager
            .OpenExeConfiguration(ConfigurationUserLevel.None);
        /// <summary>
        /// 获取config中AppSettings节对应的节点值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns></returns>
        private static string get(object keyName)
        {
            return ConfigurationManager.AppSettings[keyName.ToString()];
        }

        /// <summary>
        /// 获取config中的连接字符串
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns></returns>
        public static string GetConnectString(string keyName)
        {
            return ConfigurationManager
                .ConnectionStrings[keyName]
                .ConnectionString;
        }
        /// <summary>
        /// 获取config键值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="defaultValue">默认值(引用)</param>
        public static void Get(object keyName
            , ref object defaultValue)
        {
            #region
            string value = get(keyName);

            if (!_configCacheGroup.Contains(keyName))
            {
                if (value == null)
                { 
                    Save(keyName.ToString(), defaultValue.ToString());
                    _configCacheGroup.Add(keyName, defaultValue);
                }
                else
                { 
                    _configCacheGroup.Add(keyName, value);
                    defaultValue = value;
                }
            }
            else
                defaultValue = _configCacheGroup[keyName];
            #endregion
        }
        /// <summary>
        /// 保存App.config文件对应键值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="value">保存值</param>
        private static string Save(string keyName
            , string value)
        {
            #region
            _configuration.AppSettings.Settings.Add(keyName, value);

            lock (_lockConfig)
                _configuration.Save();

            return value;
            #endregion
        }
        /// <summary>
        /// 更新App.config文件对应键值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="value">更新值</param>
        public static void Update(object keyName, ref object newValue)
        {
            #region
            if (_configCacheGroup.Contains(keyName))
            {
                if (_configCacheGroup[keyName] != newValue)
                {
                    //此处给对应的键进行赋值，下面的save操作会应用此更新。
                    _configuration.AppSettings.Settings[keyName.ToString()].Value = newValue.ToString();

                    lock (_lockConfig)
                    {
                        _configuration.Save();
                        _configCacheGroup[keyName] = newValue;
                    }
                }
            }
            else
                ExtConsole.Write(string.Format("系统应针对{0}键先进行读取后才可保存", keyName));
            
            #endregion
        }
    

    }
}
