using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class RecvFileAck : IClientCommand
    {

        public EventHandler OnStartingDownload = null;


        public override bool Analysis()
        {
            if (OnStartingDownload != null)
                OnStartingDownload(this, null);
            return true;
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.RecvFileAck);
            return businesscommand;
        }
    }
}
