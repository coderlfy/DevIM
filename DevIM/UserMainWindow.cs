using DevIM.chat;
using DevIM.custom;
using DevIM.icon;
using DevIM.Model;
using DevIM.Test;
using DevIMDataLibrary;
using Fundation.Core;
using SocketCommunication.PipeData;
using SocketCommunication.TcpSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DevIM
{
    public partial class UserMainWindow : Form
    {
        private WinIconStatus _iconStatus;

        public WinIconStatus _IconStatus
        {
            get { return _iconStatus; }
            set { _iconStatus = value; }
        }

        private IconController _iconController = null;
        
        public UserMainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserMainWindow_Load(object sender, EventArgs e)
        {
            #region
            IconCollector.Init();

            AsyncCallback callbak = new AsyncCallback(fillTreefriend);
            MethodInvoker gd = new MethodInvoker(requestUserList);
            //异步请求中传入callback方法控制变化UI的最终结果？
            //其实上述方法应该返回bool类型，以确定返回登录是否成功。
            gd.BeginInvoke(callbak, gd);

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        private void requestUserList()
        {
            #region
            TcpClientEx tcpclient = new TcpClientEx(
                ServerInfor._Ip.ToString(), Convert.ToInt16(ServerInfor._Port));

            SendRequstFriendShip sendrequestfriendship = 
                new SendRequstFriendShip()
                    { _UserInfor = Logon._User };

            byte[] command = sendrequestfriendship.GetProtocolCommand();

            ExtConsole.WriteByteArray(command);

            tcpclient.Connect();

            tcpclient.SendToEndDevice(command);

            tcpclient.ReceiveFile();

            //RecvUserCheckResult usercheckresult = new RecvUserCheckResult();

            //tcpclient.Dispatcher(usercheckresult);

            //Console.WriteLine(usercheckresult._Result._Message);

            tcpclient.Close();
            
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void fillTreefriend(IAsyncResult result)
        {
            #region
            MethodInvoker dl_do = (MethodInvoker)result.AsyncState;
            dl_do.EndInvoke(result);


            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => {
                    this.tFriend.Nodes.Clear();
                    XmlDataDocument Xmldate = new XmlDataDocument();
                    Xmldate.Load("online.dat");
                    XmlNode root = Xmldate.DocumentElement;
                    for (int i = 0; i < root.ChildNodes.Count; i++)
                    {
                        XmlNode group = root.ChildNodes[i];
                        System.Windows.Forms.TreeNode father = new System.Windows.Forms.TreeNode();
                        father.Text = group.Attributes[GroupData.groupName].Value;
                        father.ImageIndex = 2;
                        father.SelectedImageIndex = 2;
                        this.tFriend.Nodes.Add(father);
                        fillFriendsChild(i, group);
                    }
                }));
            }

            _iconController = new IconController();
            _iconController.Bind(_IconStatus);

            //测试注册侦听
            MethodInvoker gd = new MethodInvoker(() => {
                (new ChatClient()).RegisterListen(_iconController);
            });
            gd.BeginInvoke(null, null);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fatherId"></param>
        /// <param name="father"></param>
        private void fillFriendsChild(int fatherId, XmlNode father)
        {
            #region
            for (int i = 0; i < father.ChildNodes.Count; i++)
            {
                TreeNode son = new TreeNode();
                son.Text = father.ChildNodes[i].ChildNodes[1].InnerText + "(" + father.ChildNodes[i].ChildNodes[0].InnerText + ")";
                son.Tag = father.ChildNodes[i].ChildNodes[2].InnerText;
                //ShareDate.QQName.Add(father.ChildNodes[i].ChildNodes[1].InnerText);
                //ShareDate.QQNumber.Add(father.ChildNodes[i].ChildNodes[0].InnerText);
                //if (isThisUserOnline(father.ChildNodes[i].ChildNodes[0].InnerText))
                //{
                //    son.ImageIndex = 1;
                //    son.SelectedImageIndex = 1;
                //}
                //else
                //{
                    son.ImageIndex = 0;
                    son.SelectedImageIndex = 0;
                //}
                this.tFriend.Nodes[fatherId].Nodes.Add(son);
            }
            #endregion
        }

        private string[] getUserInfor(string nodetext)
        {
            string[] userinfor = new string[2];
            int usernameindex = nodetext.IndexOf("(");
            int noindex = nodetext.IndexOf(")");

            userinfor[0] = nodetext.Substring(0, 
                usernameindex);

            userinfor[1] = nodetext.Substring(
                usernameindex, noindex - usernameindex);
            return userinfor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tFriend_DoubleClick(object sender, EventArgs e)
        {
            #region
            object tag = this.tFriend.SelectedNode.Tag;
            string[] userinfor = getUserInfor(this.tFriend.SelectedNode.Text);
            if (tag != null)
            {
                EntityTUser user = new EntityTUser(){
                    uid = tag.ToString(),
                    userid = userinfor[1],
                    userfullName = userinfor[0]
                };
                Friend newfriend = new Friend() { 
                    _User = user
                };

                Friend findfriend = FriendCollector.FindFriend(newfriend);
                if (findfriend != null)
                {
                    TrafficMsg.PostMessage(
                        int.Parse(findfriend._FrmHandle.ToString()), 
                        501, 0, 0);
                }
                else
                {
                    P2P p2pwindow = new P2P();
                    newfriend._FrmHandle = p2pwindow.Handle;

                    if (FriendCollector.Add(newfriend))
                        p2pwindow._Friend = newfriend;

                    p2pwindow.Show();
                }

            }
            #endregion
        }


    }
}
