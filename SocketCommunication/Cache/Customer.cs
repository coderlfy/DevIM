using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.Cache
{
    public class Customer
    {
        private string fullname;

        public string Fullname
        {
            get { return fullname; }
            set { fullname = value; }
        }

        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string ipaddress;

        public string IPAddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }

        private int _uId;

        public int _UId
        {
            get { return _uId; }
            set { _uId = value; }
        }

        private Socket _srcSocket;

        public Socket _SrcSocket
        {
            get { return _srcSocket; }
            set { _srcSocket = value; }
        }

        private DateTime _logonTime;
        /// <summary>
        /// 登录时刻
        /// </summary>
        public DateTime _LogonTime
        {
            get { return _logonTime; }
            set { _logonTime = value; }
        }

        private DateTime _updateTime;
        /// <summary>
        /// 更新在线时刻
        /// </summary>
        public DateTime _UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }
    }
}
