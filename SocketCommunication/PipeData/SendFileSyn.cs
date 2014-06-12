using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class SendFileSyn: ISocketCommand
    {



        public override void Analysis()
        {
            base._SourceClient.Send(
                (CommandFactory.CreateSocketCommandObject(TProtocol.SendFileAck))
                .GetProtocolCommand());
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendFileSyn);
            return businesscommand;
        }
    
    }
}
