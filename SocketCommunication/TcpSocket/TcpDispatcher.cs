
using SocketCommunication.Cache;
using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.TcpSocket
{

    public class TcpDispatcher
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
            #region
            ProtocolRule.GetTProtocol(userData._SourceData).Analysis();

            /*
            if (userData._SourceData[1] == 0x10)
            {
                this._clientSocket.Send(new byte[] { 0x03, 0x11, 0x13 });
            }
             * */
            #endregion
        }

        /// <summary>
        /// 有一帧内有多条命令
        /// </summary>
        private void split()
        {
            #region
            #endregion
        }
    }
}
