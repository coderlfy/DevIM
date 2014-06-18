using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class MsgResultModel
    {
        private bool success;

        public bool _Success
        {
            get { return success; }
            set { success = value; }
        }

        private string message;

        public string _Message
        {
            get { return message; }
            set { message = value; }
        }

    }

}
