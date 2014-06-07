/****************************************
###创建人：lify
###创建时间：2012-06-29
###公司：山西博华科技有限公司
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

namespace IMServer.common
{
    class Log
    {
        private const object _LogLockObject = null;

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
        public void Listen(string dirName, string logFilename)
        {
            #region
            log = new LogBusiness(dirName, logFilename);


            Application.ThreadException += 
                new ThreadExceptionEventHandler(
                    Log.Application_ThreadException);
            #endregion
        }
    }
}
