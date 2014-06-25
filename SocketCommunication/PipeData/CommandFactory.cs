using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class CommandFactory
    {
        public static IServerCommand CreateServerCommandObject(
            TProtocol tprotocol)
        {
            #region
            IServerCommand orgdata = null;
            switch (tprotocol)
            { 

                case TProtocol.SendFileSyn:
                    orgdata = new SendFileSyn();
                    break;
                case TProtocol.SendUserValidCheck:
                    orgdata = new SendUserValidCheck();
                    break;
                case TProtocol.SendRequstFriendShip:
                    orgdata = new SendRequstFriendShip();
                    break;
                case TProtocol.SendChatContent:
                    orgdata = new SendChatContent();
                    break;
                case TProtocol.SendRegisterClientListen:
                    orgdata = new SendRegisterClientListen();
                    break;
                case TProtocol.SendOnlineMarkup:
                    orgdata = new SendOnlineMarkup();
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
        public static IClientCommand CreateClientCommandObject(
            TProtocol tprotocol)
        {
            #region
            IClientCommand orgdata = null;
            switch (tprotocol)
            {
                case TProtocol.RecvChatContent:
                    orgdata = new RecvChatContent();
                    break;
            }
            return orgdata;
            #endregion
        }

    }
}
