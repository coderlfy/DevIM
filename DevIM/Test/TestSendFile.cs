using DevIM.socket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevIM.Test
{
    public partial class TestSendFile : Form
    {
        public TestSendFile()
        {
            InitializeComponent();
        }

        private void btnSelectSendFile_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelSend_Click(object sender, EventArgs e)
        {

        }

        private void btnStartSend_Click(object sender, EventArgs e)
        {
            TcpClient tcpclient = new TcpClient("192.168.159.104", 1005);
            byte[] data = new byte[] { 0x32, 0x31, 0x30};

            tcpclient.SendToEndDevice(data);
            
        }

    }
}
