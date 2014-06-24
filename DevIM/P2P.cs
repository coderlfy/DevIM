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
        public P2P()
        {
            InitializeComponent();
        }

        private void btnStartSend_Click(object sender, EventArgs e)
        {
            string destuid = (Logon._User.uid == "1") ? "2" : "1";

            string content = string.Format("去你妈的用户：{0}", destuid);
            ChatClient client = new ChatClient();
            client.Send(content, int.Parse(destuid));
        }

    }
}
