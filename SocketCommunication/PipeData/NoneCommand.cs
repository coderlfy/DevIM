using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class NoneCommand: ISocketOrginalData
    {
        public List<byte> _AfterDecodeData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Analysis()
        {
            throw new NotImplementedException();
        }
    }
}
