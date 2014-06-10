using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.Error
{
    public class ErrorEventArgs : EventArgs
    {
        private Exception exception;

        public Exception SocketException
        {
            get { return exception; }
            set { exception = value; }
        }
        
    }
    
}
