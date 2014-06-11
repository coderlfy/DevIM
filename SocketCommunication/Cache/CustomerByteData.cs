using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.Cache
{
    public class CustomerByteData
    {
        private Customer source;

        public Customer _FromClient
        {
            get { return source; }
            set { source = value; }
        }

        private List<byte> sourceData;

        public List<byte> _SourceData
        {
            get { return sourceData; }
            set { sourceData = value; }
        }


        private List<byte[]> dispatcherData;

        public List<byte[]> _DispatcherData
        {
            get { return dispatcherData; }
            set { dispatcherData = value; }
        }

    }
}
