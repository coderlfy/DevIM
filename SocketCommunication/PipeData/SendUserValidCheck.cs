using DevIMBusiness;
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

        public override bool Analysis()
        {
            List<string> analysisinfor = base.Split(2);
            TUserData userdata = (new TUserBusiness())
                .GetUserCheck(analysisinfor[0], analysisinfor[1]);

            bool issuccess = false;
            string message = "";

            System.Data.DataRow dr = null;
            if (userdata.Tables[0].Rows.Count == 1)
            {
                dr = userdata.Tables[0].Rows[0];
                issuccess = true;
                message = string.Format("{0}-{1}", dr[TUserData.uid], dr[TUserData.userfullName]);
            }
            else
                message = "帐号异常！";

            RecvUserCheckResult cmd = new RecvUserCheckResult();
            cmd._Result = new MsgResultModel()
            {
                _Success = issuccess,
                _Message = message
            };
            Console.WriteLine("用户名：{0}", analysisinfor[0]);
            base._SourceClient.Send(cmd.GetProtocolCommand());
            return true;
        }
    }
}
