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
        public override void Run()
        {
            _clientCommand.Analysis();
        }
    }
}
