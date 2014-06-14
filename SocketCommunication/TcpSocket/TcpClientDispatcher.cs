using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.TcpSocket
{
    class TcpClientDispatcher : TcpDispatcher
    {

        public override void Run()
        {
            IClientCommand socketcommand = ProtocolRule.GetClientCommand(new List<byte>());
            //socketcommand._SourceClient = this._clientSocket;
            socketcommand.Analysis();
        }
    }
}
