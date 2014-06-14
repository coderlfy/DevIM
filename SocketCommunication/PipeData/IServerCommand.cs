using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.PipeData
{
    public abstract class IServerCommand : ISocketCommand
    {

        private Socket _sourceClient;

        public Socket _SourceClient
        {
            get { return _sourceClient; }
            set { _sourceClient = value; }
        }


        //public abstract void Analysis();

        //public abstract List<byte> GetCommand();
    }
}
