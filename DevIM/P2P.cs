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

            this.rtbHistory.AppendText(string.Format(
                "To:{0}--{1}\r\n{2}\r\n", 
                this._Friend._User.userfullName, 
                content, 
                DateTime.Now));
            #endregion
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool FlashWindow( IntPtr hWnd, bool bInvert );

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
                    viewFromMessages();
                    FlashWindow(this.Handle, true);
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
                this._Friend._User.userfullName, 
                this._Friend._User.userid);

            if (this._Friend._MessageMode == MessageMode.None)
            {
                viewFromMessages();
                this._Friend._MessageMode = MessageMode.HasPop;
            }
            this.tbSendContent.Focus();
            #endregion
        }
        /// <summary>
        /// 显示别人发来的信息
        /// </summary>
        private void viewFromMessages()
        {
            #region
            foreach (ChatMessage msg in _Friend._Messages)
                this.rtbHistory.AppendText(string.Format("from:{0}--{1}\r\n{2}\r\n",
                    _Friend._User.userfullName,
                    msg._Content,
                    msg._RecvTime));
            _Friend._Messages.Clear();
            #endregion
        }
    }
}
