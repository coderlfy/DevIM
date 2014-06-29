using DevIMDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.chat
{
    public class Friend
    {
        private EntityTUser _user;

        public EntityTUser _User
        {
            get { return _user; }
            set { _user = value; }
        }
        
        private IntPtr _frmHandle;

        public IntPtr _FrmHandle
        {
            get { return _frmHandle; }
            set { _frmHandle = value; }
        }

        private string _message;

        public string _Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private DateTime _recvMsgTime;

        public DateTime _RecvMsgTime
        {
            get { return _recvMsgTime; }
            set { _recvMsgTime = value; }
        }
        
        
    }
}
