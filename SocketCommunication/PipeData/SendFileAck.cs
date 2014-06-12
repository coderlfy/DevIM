using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class SendFileAck : ISocketCommand
    {



        public override void Analysis()
        {
            
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendFileAck);
            return businesscommand;
        }
    }
}
