using DevIM.chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevIM
{
    public partial class P2P : Form
    {
        private Friend _friend;

        public Friend _Friend
        {
            get { return _friend; }
            set { _friend = value; }
        }
        
        public P2P()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartSend_Click(object sender, EventArgs e)
        {
            #region
            string destuid = (Logon._User.uid == "1") ? "2" : "1";

            string content = this.tbSendContent.Text;
            ChatClient client = new ChatClient();
            client.Send(content, int.Parse(destuid));
            this.tbSendContent.Clear();

            this.rtbHistory.AppendText(string.Format("To:{0}--{1}\r\n{2}\r\n", this._Friend._User.userfullName, content, DateTime.Now));
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void DefWndProc(
            ref System.Windows.Forms.Message m)
        {
            #region
            switch (m.Msg)
            {
                case 500://向窗体中添加消息
                    string MsgToAdd;
                    //MsgToAdd = ShareDate.Msg[m.WParam.ToInt32()].ToString();
                    //AddMsg(MsgToAdd);

                    this.rtbHistory.AppendText(string.Format("from:{0}--{1}\r\n{2}\r\n",
                        _Friend._User.userfullName, 
                        _Friend._Message, 
                        _Friend._RecvMsgTime));
                    break;
                case 501://将本窗体激活
                    this.Activate();
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2P_FormClosing(
            object sender, FormClosingEventArgs e)
        {
            #region
            FriendCollector.Remove(this._Friend);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P2P_Load(object sender, EventArgs e)
        {
            #region
            this.Text = string.Format("与{0}({1})聊天中……", 
                this._Friend._User.userfullName, this._Friend._User.userid);
            #endregion
        }
    }
}
