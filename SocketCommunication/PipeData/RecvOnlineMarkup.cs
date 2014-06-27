using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class RecvOnlineMarkup : IClientCommand
    {
        public override bool Analysis()
        {
            return true;
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.RecvOnlineMarkup);
            return businesscommand;
        }
    }
}
