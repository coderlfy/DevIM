using System;
using System.Collections.Generic;
using System.Linq;
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
        
    }
}
