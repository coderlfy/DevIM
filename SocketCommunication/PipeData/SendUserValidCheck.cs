using DevIMDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendUserValidCheck : IServerCommand
    {
        private EntityTUser _userInfor;

        public EntityTUser _UserInfor
        {
            get { return _userInfor; }
            set { _userInfor = value; }
        }
        
        public override List<byte> GetCommand()
        {
            string content = string.Format("{0};{1}",
                this._UserInfor.userid,
                this._UserInfor.userpwd
                );

            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendUserValidCheck);
            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;
        }

        public override void Analysis()
        {
            List<string> analysisinfor = base.Split(2);

            RecvUserCheckResult cmd = new RecvUserCheckResult();
            cmd._Result = new MsgResultModel()
            {
                _Success = (analysisinfor[0] == "00000"),
                _Message = (analysisinfor[0] == "00000") ? "通过！" : "验证失败！"
            };
            base._SourceClient.Send(cmd.GetProtocolCommand());

        }
    }
}
