using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.TcpSocket
{
    class TcpClientDispatcher : TcpDispatcher
    {
        private IClientCommand _clientCommand;

        public IClientCommand _ClientCommand
        {
            get { return _clientCommand; }
            set { _clientCommand = value; }
        }
        
        public TcpClientDispatcher(IClientCommand socketcommand)
        {
            this._ClientCommand = socketcommand;
        }

        public TcpClientDispatcher()
        {
            
        }

        public override bool Run()
        {
            return _clientCommand.Analysis();
        }

        public void RunAll(List<byte> data)
        {
            IClientCommand socketcommand = ProtocolRule.GetClientCommand(data);
            socketcommand._AfterDecodeData = data;
            socketcommand._AfterDecodeData.RemoveAt(0);
            socketcommand._AfterDecodeData.RemoveAt(0);
            socketcommand._AfterDecodeData.RemoveAt(socketcommand._AfterDecodeData.Count - 1);
            socketcommand.Analysis();

        }
    }
}
