using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.Model
{
    public class ServerInfor
    {

        public const string KeyNameServerIP = "ServerIP";
        public const string KeyNameServerPort = "ServerPort";
        private static object _ip = "192.168.159.104";

        public static object _Ip
        {
            get { return _ip; }
            set { _ip = value; }
        }


        private static object _port = 1005;

        public static object _Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public static void GetParamsFromConfig()
        {
            Config.Get(KeyNameServerPort, ref _port);
            Config.Get(KeyNameServerIP, ref _ip);
        }
    }
}
