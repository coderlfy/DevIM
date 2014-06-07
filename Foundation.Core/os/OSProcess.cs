using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public class OSProcess
    {
        private static string _processName = Process.GetCurrentProcess().ProcessName;
        /// <summary>
        /// 只允许单进程开启应用
        /// </summary>
        public static void HasSingle()
        {
            #region
            if ((Process.GetProcessesByName(_processName)).GetUpperBound(0) > 0)
            {
                ExtMessage.ShowError("程序已运行，请查看任务管理器中是否存在" + _processName + ".exe，\r\n然后再确认是否在当前用户下要运行该进程！");
                System.Environment.Exit(0);
            }
            #endregion
        }
        /// <summary>
        /// 获取应用程序的全路径（比如c:/software/aaa.exe）
        /// </summary>
        /// <returns></returns>
        public static string GetFullPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            return string.Format("{0}{1}.exe", path, _processName);
        }

        /// <summary>
        /// 获取应用程序的exe名称（不带.exe）
        /// </summary>
        /// <returns></returns>
        public static string GetExeName()
        {
            return _processName;
        }
    }
}
