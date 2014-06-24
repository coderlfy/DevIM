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

        public override void Analysis()
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
            //在数据库中验证用户合法性？？？
            /*
            RecvUserCheckResult cmd = new RecvUserCheckResult();
            cmd._Result = new MsgResultModel()
            {
                _Success = (analysisinfor[0] == "00000"),
                _Message = (analysisinfor[0] == "00000") ? "通过！" : "验证失败！"
            };
            Console.WriteLine("用户名：{0}", analysisinfor[0]);
             * */
            //可返回确认中转收到命令
            //base._SourceClient.Send(cmd.GetProtocolCommand());
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
