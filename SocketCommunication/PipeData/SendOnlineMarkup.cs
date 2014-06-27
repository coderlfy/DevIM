using DevIMDataLibrary;
using SocketCommunication.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendOnlineMarkup : IServerCommand
    {
        private EntityTUser _userInfor;
        private static object lockcustomer = true;
        public EntityTUser _UserInfor
        {
            get { return _userInfor; }
            set { _userInfor = value; }
        }

        public override bool Analysis()
        {
            lock (lockcustomer)
            { 
                List<string> analysisinfor = base.Split(1);
                IPEndPoint endremotepoint = (System.Net.IPEndPoint)base._SourceClient.RemoteEndPoint;
                Customer customer = new Customer()
                {
                    _UId = int.Parse(analysisinfor[0]),
                    IPAddress = endremotepoint.Address.ToString(),
                    Port = endremotepoint.Port,
                    _SrcSocket = base._SourceClient
                };
                //必须拿同通道的socket来发送在线命令
            
                CustomerCollector.UpdateUserTime(customer);
            }
            return true;
        }

        public override List<byte> GetCommand()
        {
            string content = this._UserInfor.uid;


            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendOnlineMarkup);
            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;
        }
    }
}
