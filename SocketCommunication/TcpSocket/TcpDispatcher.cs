
using SocketCommunication.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.TcpSocket
{

    class TcpDispatcher
    {
        private Socket _clientSocket = null;
        private CustomerByteData userData;

        public CustomerByteData _UserData
        {
            get { return userData; }
            set { userData = value; }
        }
        
        public TcpDispatcher(Socket clientSocket)
        {
            this._clientSocket = clientSocket;
        }
        /// <summary>
        /// 公开接口进行批处理
        /// </summary>
        public void Run()
        {
            if (userData._SourceData[1] == 0x10)
            {
                this._clientSocket.Send(new byte[] { 0x03, 0x11, 0x13 });
            }
        }
        /// <summary>
        /// 验证数据格式
        /// </summary>
        /// <returns></returns>
        private bool verify()
        {
            return true;
        }

        private void split()
        { 
            
        }
    }
}
