using DevIM.custom;
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DevIM
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //zh
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CustomConfig.GetSystemParameters();
            LogInterface.Listen(CustomConfig.LogDirectoryName.ToString());

            Application.Run(new Logon());

        }
    }
}
