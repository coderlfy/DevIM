using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIMDataLibrary
{
    public class ChatContent
    {
        private string _text;

        public string _Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private int _fromUId;

        public int _FromUID
        {
            get { return _fromUId; }
            set { _fromUId = value; }
        }

        private int _toUId;

        public int _ToUId
        {
            get { return _toUId; }
            set { _toUId = value; }
        }
        

        private DateTime _fromSendoutTime;

        public DateTime _FromSendoutTime
        {
            get { return _fromSendoutTime; }
            set { _fromSendoutTime = value; }
        }

    }
}
