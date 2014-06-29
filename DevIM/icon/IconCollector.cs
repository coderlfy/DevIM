using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DevIM.icon
{
    class IconCollector
    {
        public static List<Icon> _StatusIcons = new List<Icon>();
        private static List<string> _headnames = new List<string>();
        public static Icon Get(TIcon iconType)
        {
            switch (iconType)
            { 
                case TIcon.Logining:
                    return _StatusIcons[0];
                case TIcon.NULL:
                    return _StatusIcons[1];
            }
            return null;
        }

        public static void initHeadName()
        {
            _headnames.Clear();
            _headnames.Add("msg.ico");
            _headnames.Add("default.ico");
        }

        public static void Init()
        {
            initHeadName();
            _StatusIcons.Clear();
            string filenameFormat = AppDomain.CurrentDomain.BaseDirectory + "head\\{0}";

            foreach (string name in _headnames)
            {
                string filename = string.Format(filenameFormat, name);
                _StatusIcons.Add(new Icon(filename));
            }

        }
    }
}
