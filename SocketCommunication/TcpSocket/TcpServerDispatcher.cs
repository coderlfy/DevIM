using SocketCommunication.Cache;
using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.TcpSocket
{
    public class TcpServerDispatcher : TcpDispatcher
    {
        private Socket _clientSocket = null;

        private CustomerByteData userData;

        public CustomerByteData _UserData
        {
            get { return userData; }
            set { userData = value; }
        }
        public TcpServerDispatcher(Socket clientSocket)
        {
            this._clientSocket = clientSocket;
        }
        public override bool Run()
        {
            #region
            IServerCommand socketcommand = ProtocolRule.GetServerCommand(userData._SourceData);
            socketcommand._SourceClient = this._clientSocket;
            //此处需要将sourcedata进行验证，解码后的数据只包括业务数据（？？？）
            socketcommand._AfterDecodeData = userData._SourceData;
            socketcommand._AfterDecodeData.RemoveAt(0);
            socketcommand._AfterDecodeData.RemoveAt(0);
            socketcommand._AfterDecodeData.RemoveAt(socketcommand._AfterDecodeData.Count - 1);
            return socketcommand.Analysis();

            #endregion
        }

    }
}
