using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class NoneCommand: ISocketCommand
    {



        public override void Analysis()
        {
            throw new NotImplementedException();
        }

        public override List<byte> GetCommand()
        {
            throw new NotImplementedException();
        }
    }
}
