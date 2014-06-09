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
            MethodInvoker gd = new MethodInvoker(() =>
            {
                TcpClient tcpclient = new TcpClient("192.168.159.103", 1005);
                tcpclient.OnStartingDownload += new EventHandler(StartingDownload);
                byte[] data = new byte[] { 0x03, 0x10, 0x13 };

                tcpclient.Connect();

                tcpclient.SendToEndDevice(data);
                tcpclient.receive();
                tcpclient.Dispatcher();
                tcpclient.Close();
            });

            gd.BeginInvoke(null, null);
        }

        private void StartingDownload(object sender, EventArgs e)
        {
            this.lbSendStatus.Text = "开始传输";
        }
    }
}
