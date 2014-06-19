using DevIMDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class SendRequstFriendShip : IServerCommand
    {
        private EntityTUser _userInfor;

        public EntityTUser _UserInfor
        {
            get { return _userInfor; }
            set { _userInfor = value; }
        }

        public override void Analysis()
        {
            throw new NotImplementedException();
        }

        public override List<byte> GetCommand()
        {
            string content = string.Format("{0};{1};{2}",
                this._UserInfor.uid,
                this._UserInfor.userid,
                //密码应通过其他加密手段
                this._UserInfor.userpwd
                );

            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.SendRequstFriendShip);
            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;

        }
    }
}
