﻿using DevIM.chat;
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
        public static List<EntityTUser> _AllFriends = null;
        private IconController _iconController = null;
        public static int _FrmHandle = 0;
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
            _AllFriends = new List<EntityTUser>();

            IconCollector.Init();

            _iconController = new IconController();
            _iconController.Bind(_IconStatus);
            _iconController._IconStatus.OnFlashEventHandler += new EventHandler(IconStatus_OnFlash);

            _FrmHandle = this.Handle.ToInt32();
            AsyncCallback callbak = new AsyncCallback(fillTreefriend);
            MethodInvoker gd = new MethodInvoker(requestUserList);
            //异步请求中传入callback方法控制变化UI的最终结果？
            //其实上述方法应该返回bool类型，以确定返回登录是否成功。
            gd.BeginInvoke(callbak, gd);

            #endregion
        }

        private void IconStatus_OnFlash(object sender, EventArgs e)
        {
            foreach(Friend friend in FriendCollector._Friends)
            {
                if (friend._MessageMode == MessageMode.None)
                {
                    P2P p2pwindow = new P2P();
                    p2pwindow._Friend = friend;
                    p2pwindow._Friend._FrmHandle = p2pwindow.Handle;
                    p2pwindow.Show();

                }
            }
            _iconController.Reset();
            _iconController._IconStatus.SetIconStatusMode(IconStatusMode.Normal);
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
                EntityTUser frienduser = new EntityTUser()
                {
                    uid = father.ChildNodes[i].ChildNodes[2].InnerText,
                    userid = father.ChildNodes[i].ChildNodes[0].InnerText,
                    userfullName = father.ChildNodes[i].ChildNodes[1].InnerText
                };

                TreeNode son = new TreeNode();
                son.Text = frienduser.userfullName + "(" + frienduser.userid + ")";
                son.Tag = frienduser.uid;

                _AllFriends.Add(frienduser);
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
        public static EntityTUser GetFriendUser(string uid)
        {
            foreach (EntityTUser user in _AllFriends)
                if (user.uid == uid)
                    return user;
            return null;
        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodetext"></param>
        /// <returns></returns>
        private string[] getUserInfor(string nodetext)
        {
            #region
            string[] userinfor = new string[2];
            int usernameindex = nodetext.IndexOf("(");
            int noindex = nodetext.IndexOf(")");

            userinfor[0] = nodetext.Substring(0, 
                usernameindex);

            userinfor[1] = nodetext.Substring(
                usernameindex, noindex - usernameindex);
            return userinfor;
            #endregion
        }
         * */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tFriend_DoubleClick(object sender, EventArgs e)
        {
            #region
            object tag = this.tFriend.SelectedNode.Tag;
            if (tag != null)
            {
                EntityTUser user = GetFriendUser(tag.ToString());

                Friend newfriend = new Friend()
                {
                    _User = user,
                    _Messages = new List<ChatMessage>()
                };

                Friend findfriend = FriendCollector.FindFriend(newfriend);
                if (findfriend != null)
                {
                    if (findfriend._MessageMode == MessageMode.HasPop)
                    {
                        TrafficMsg.PostMessage(
                            int.Parse(findfriend._FrmHandle.ToString()),
                            501, 0, 0);
                    }
                    else
                    {
                        int nonepopcount = 0;
                        foreach (Friend friend in FriendCollector._Friends)
                        {
                            if (friend._MessageMode == MessageMode.None)
                                nonepopcount++;
                        }
                        if (nonepopcount == 1)
                            this._iconController.Reset();

                        P2P p2pwindow = new P2P();
                        p2pwindow._Friend = findfriend;
                        p2pwindow._Friend._FrmHandle = p2pwindow.Handle;
                        p2pwindow.Show();

                    }
                }
                else
                {
                    P2P p2pwindow = new P2P();
                    newfriend._FrmHandle = p2pwindow.Handle;

                    if (FriendCollector.Add(newfriend))
                    {
                        p2pwindow._Friend = newfriend;
                        p2pwindow._Friend._MessageMode = MessageMode.HasPop;
                    }

                    p2pwindow.Show();
                }

            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            #region
            switch (m.Msg)
            {
                case 500://播放声音
                    //PlaySound.play(m.WParam.ToInt32());
                    break;
                case 501://闪烁图标
                    _iconController.StartFlash();
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
            #endregion
        }

    }
}
