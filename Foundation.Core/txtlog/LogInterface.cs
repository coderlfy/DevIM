/****************************************
###创建人：lify
###创建时间：2012-06-29
###公司：ICat科技
###最后修改时间：2012-06-29
###最后修改人：lify
###修改摘要：
****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fundation.Core;
using System.Threading;

namespace Fundation.Core
{
    public class LogInterface
    {
        private const object _LogLockObject = null;
        /// <summary>
        /// 日志文件名称
        /// </summary>
        private static String LogFileName
        {
            #region
            get
            {
                return DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            #endregion
        }

        private static string dirName;

        public static string DirName
        {
            get { return dirName; }
            set { dirName = value; }
        }
        
        private static LogBusiness log = null;
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="error"></param>
        public static void Write(string error)
        {
            #region
            lock (_LogLockObject)
            {
                log = new LogBusiness(dirName, LogFileName);
                string logTemplate = "Error occurs in {0}\r\n{1}";
                string logContent = String.Format(logTemplate, DateTime.Now.ToString(), error);
                log.writefile(logContent);
            }
            #endregion
        }

        /// <summary>
        /// 系统错误捕捉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(
            object sender, ThreadExceptionEventArgs e)
        {
            #region
            Write(e.Exception.ToString());
            #endregion
        }
        /// <summary>
        /// 安排事件侦听错误
        /// </summary>
        public static void Listen(string dirName)
        {
            #region

            DirName = dirName;
            Application.ThreadException += 
                new ThreadExceptionEventHandler(
                    LogInterface.Application_ThreadException);
            #endregion
        }
    }
}
