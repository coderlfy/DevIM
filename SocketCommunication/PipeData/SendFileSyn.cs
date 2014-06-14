using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendFileSyn: IServerCommand
    {


        public EventHandler OnStartingDownload = null;

        public override void Analysis()
        {
            base._SourceClient.Send((new RecvFileAck())
                .GetProtocolCommand());

            if (OnStartingDownload != null)
                OnStartingDownload(this, null);

        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendFileSyn);
            return businesscommand;
        }
    
    }
}
