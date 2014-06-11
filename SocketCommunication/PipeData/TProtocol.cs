using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    enum TProtocol
    {
        SendFileSyn=0x10,
        SendFileAck=0x11,
        SendFileRefuse=0x12
    }
}
