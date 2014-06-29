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

        private List<ChatMessage> _messages;

        public List<ChatMessage> _Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        private MessageMode _messageMode = MessageMode.None;

        public MessageMode _MessageMode
        {
            get { return _messageMode; }
            set { _messageMode = value; }
        }
        
    }
}
