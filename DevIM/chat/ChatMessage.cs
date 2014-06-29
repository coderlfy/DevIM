using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.chat
{
    public class ChatMessage
    {
        private string _content;

        public string _Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private DateTime _recvTime;

        public DateTime _RecvTime
        {
            get { return _recvTime; }
            set { _recvTime = value; }
        }

    }
}
