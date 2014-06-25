using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public enum TProtocol
    {
        Head = 0x03,
        SendFileSyn = 0x10,
        RecvFileAck = 0x11,
        SendFileRefuse = 0x12,
        Tail = 0x13,
        SendUserValidCheck = 0x04,
        RecvUserCheckResult = 0x05,
        SendRequstFriendShip = 0x06,
        SendChatContent = 0x14,
        SendRegisterClientListen = 0x15,
        RecvChatContent = 0x16,
        SendOnlineMarkup = 0x17,
        RecvOnlineMarkup = 0x18
    }
}
