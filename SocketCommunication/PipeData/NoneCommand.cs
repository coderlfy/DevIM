using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class NoneCommand: ISocketCommand
    {



        public override bool Analysis()
        {
            throw new NotImplementedException();
        }

        public override List<byte> GetCommand()
        {
            throw new NotImplementedException();
        }
    }
}
