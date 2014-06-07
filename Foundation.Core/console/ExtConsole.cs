/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public class ExtConsole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public static void WriteWithColor(string text)
        {
            #region
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ColorControler.GetRandomColor();
            Console.WriteLine(text);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            #region
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            #endregion
        }
    }
}
