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
        public override void Run()
        {
            #region
            IServerCommand socketcommand = ProtocolRule.GetServerCommand(userData._SourceData);
            socketcommand._SourceClient = this._clientSocket;
            socketcommand.Analysis();

            #endregion
        }

    }
}
