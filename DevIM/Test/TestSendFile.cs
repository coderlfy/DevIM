
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
                TcpClient tcpclient = new TcpClient("192.168.159.104", 1005);

                SendFileSyn transfilesyn = new SendFileSyn();
                

                tcpclient.Connect();

                tcpclient.SendToEndDevice(transfilesyn.GetProtocolCommand());

                tcpclient.receive();

                RecvFileAck transfileack = new RecvFileAck();
                transfileack.OnStartingDownload += new EventHandler(StartingDownload);
                tcpclient.Dispatcher(transfileack);

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
