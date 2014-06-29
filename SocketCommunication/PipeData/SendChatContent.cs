using DevIMDataLibrary;
using SocketCommunication.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendChatContent : IServerCommand
    {
        private ChatContent _content;

        public ChatContent _Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public override bool Analysis()
        {
            List<string> analysisinfor = base.Split(3);

            List<Customer> customers = CustomerCollector.FindCustomers(analysisinfor[1]);

            if (customers != null && customers.Count>0)
            {
                RecvChatContent cmd = new RecvChatContent();
                cmd._Content = new ChatContent()
                {
                    _FromUID = int.Parse(analysisinfor[0]),
                    _ToUId = int.Parse(analysisinfor[1]),
                    _Text = analysisinfor[2],
                    _FromSendoutTime = DateTime.Now
                };
                byte[] bytecmd = cmd.GetProtocolCommand();

                foreach (Customer client in customers)
                    client._SrcSocket.Send(bytecmd);
            }

            return true;
        }

        public override List<byte> GetCommand()
        {
            string content = string.Format("{0};{1};{2}",
                this._Content._FromUID,
                this._Content._ToUId,
                this._Content._Text
                );

            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendChatContent);
            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;
        }
    }
}
