using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class CommandFactory
    {
        public static IServerCommand CreateSocketCommandObject(
            TProtocol tprotocol)
        {
            #region
            IServerCommand orgdata = null;
            switch (tprotocol)
            { 
                //case TProtocol.SendFileAck:
                //    orgdata = new SendFileAck();
                //    break;
                case TProtocol.SendFileSyn:
                    orgdata = new SendFileSyn();
                    break;
                
            }
            return orgdata;
                    /*
            ISocketCommand orgdata = null;
            if (verify(pipeData))
                orgdata = new SendFileAck();
            else
                orgdata = new NoneCommand();
            orgdata._AfterDecodeData = decode(pipeData);
            return orgdata;
                     * */
            #endregion
        }

    }
}
