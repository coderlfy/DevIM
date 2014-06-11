using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    interface ISocketOrginalData
    {
        List<byte> _AfterDecodeData { get; set; }

        void Analysis();

        byte[] GetCommand();
    }
}
