using DevIMDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class RecvChatContent : IClientCommand
    {
        private ChatContent _content;

        public ChatContent _Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public override bool Analysis()
        {
            List<string> analysisinfor = base.Split(2);

            if (analysisinfor != null)
            {
                this._Content = new ChatContent();
                this._Content._FromUID = int.Parse(analysisinfor[0]);
                this._Content._Text = analysisinfor[1];
            }

            return true;
        }

        public override List<byte> GetCommand()
        {
            string content = string.Format("{0};{1}{2}",
                this._Content._FromUID,
                //this._Content._ToUId,
                this._Content._Text,
                this._Content._FromSendoutTime.ToString("yyyy-MM-dd HH:mm:ss")
                );

            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.RecvChatContent);
            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;
        }
    }
}
