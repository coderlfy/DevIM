using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendFileSyn: IServerCommand
    {



        public override bool Analysis()
        {
            base._SourceClient.Send((new RecvFileAck())
                .GetProtocolCommand());

            return true;
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendFileSyn);
            return businesscommand;
        }
    
    }
}
