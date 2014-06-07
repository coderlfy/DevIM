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
    public class ColorControler
    {
        private static ConsoleColor[] _colors = new ConsoleColor[5] { 
            ConsoleColor.Blue, 
            ConsoleColor.Green, 
            ConsoleColor.Red, 
            ConsoleColor.Yellow, 
            ConsoleColor.Magenta 
        };

        private static ConsoleColor _lastColor = ConsoleColor.DarkBlue;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ConsoleColor GetRandomColor()
        {
            ConsoleColor currentcolor;
            while (true)
            { 
                int random = DateTime.Now.Millisecond%5;
                currentcolor = _colors[random];
                if (_lastColor != currentcolor)
                {
                    _lastColor = currentcolor;
                    break;
                }
            }
            return currentcolor;
        }
    }
}
