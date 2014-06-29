using DevIMBusiness;
using DevIMDataLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

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

        private StringBuilder addGroupToXML(string uid)
        {
            #region
            GroupBusiness groupbusiness = new GroupBusiness();
            GroupData groupdata = groupbusiness.GetGroupByUid(uid);
            StringBuilder FriendInf = new StringBuilder();
            FriendInf.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            FriendInf.Append("<root>");
            if (groupdata == null)
            {
                FriendInf.Append("<FriendGroup groupName=\"没有分组\"></FriendGroup>");
            }
            else
            {
                for (int i = 0; i < groupdata.Tables[0].Rows.Count; i++)
                {
                    FriendInf.Append("<FriendGroup groupName=\"");
                    FriendInf.Append(groupdata.Tables[0].Rows[i][GroupData.groupName].ToString().Trim());
                    FriendInf.Append("\"></FriendGroup>");
                }
            }
            FriendInf.Append("</root>");
            return FriendInf;
            #endregion
        }

        private void makeFriendsXML(string path, string uid)
        {
            #region
            
            StreamWriter temp = new StreamWriter(path);

            temp.Write(addGroupToXML(uid).ToString());
            temp.Close();

            addFriendsToXML(path, uid);
            #endregion
        }

        private void addFriendsToXML(string path, string uid)
        {
            #region
            TUserBusiness userbusiness = new TUserBusiness();
            DataSet friends = userbusiness.GetFriendsByGroup(uid);
            XmlDataDocument temp = new XmlDataDocument();
            temp.Load(path);

            XmlNodeList AllGroup = temp.SelectSingleNode("root").ChildNodes;
            foreach (XmlNode node in AllGroup)
            {
                string filter = string.Format("{0}='{1}'",
                    GroupData.groupName, node.Attributes["groupName"].Value);
                DataRow[] drarr = friends.Tables[0].Select(filter);
                foreach(DataRow dr in drarr)
                {
                    XmlElement friend = temp.CreateElement("friend");
                    XmlElement FriendNumber = temp.CreateElement("FriendNumber");
                    XmlElement FriendName = temp.CreateElement("FriendName");
                    XmlElement friendid = temp.CreateElement("FriendId");

                    FriendName.InnerText = dr["friendFullname"].ToString().Trim();
                    FriendNumber.InnerText = dr["friendQQ"].ToString().Trim();
                    friendid.InnerText = dr["friendId"].ToString().Trim();
                    friend.AppendChild(FriendNumber);
                    friend.AppendChild(FriendName);
                    friend.AppendChild(friendid);
                    node.AppendChild(friend);
                    //break;
                }
            }
            temp.Save(path);
            #endregion
        }

        public override bool Analysis()
        {
            #region
            List<string> analysisinfor = base.Split(3);
            string path = string.Format("temp\\{0}.xml", analysisinfor[0]);
            //构造xml好友组
            makeFriendsXML(path, analysisinfor[0]);
            //发送给
            base._SourceClient.SendFile(path);
            base._SourceClient.Close();

            return false;
            #endregion
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
