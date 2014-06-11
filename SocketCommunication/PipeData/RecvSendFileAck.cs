using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class RecvSendFileAck : ISocketOrginalData
    {
        

        void ISocketOrginalData.Analysis()
        {
            throw new NotImplementedException();
        }

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
    }
}
