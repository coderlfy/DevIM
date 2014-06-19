using DevIM.custom;
using DevIM.Model;
using DevIM.Test;
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

namespace DevIM
{
    public partial class UserMainWindow : Form
    {
        public UserMainWindow()
        {
            InitializeComponent();
        }

        /*
        private void btnTestSend_Click(object sender, EventArgs e)
        {
            (new TestSendFile()).ShowDialog();
        }
        */
        private void UserMainWindow_Load(object sender, EventArgs e)
        {

        }

        public void RequestUserList()
        {
            #region

            TcpClient tcpclient = new TcpClient(
                ServerInfor._Ip, ServerInfor._Port);

            SendRequstFriendShip sendrequestfriendship = 
                new SendRequstFriendShip()
                    { _UserInfor = Logon._User };

            byte[] command = sendrequestfriendship.GetProtocolCommand();

            //下述5行代码可复用，用来显示发送指令是否预期
            StringBuilder viewcontent = new StringBuilder();
            for (int i = 0; i < command.Length; i++)
            {
                byte temp = command[i];
                viewcontent.Append(string.Format("0x{0} ", temp.ToString("X2")));
            }

            tcpclient.Connect();

            tcpclient.SendToEndDevice(command);

            //tcpclient.Receive();

            //RecvUserCheckResult usercheckresult = new RecvUserCheckResult();

            //tcpclient.Dispatcher(usercheckresult);

            //Console.WriteLine(usercheckresult._Result._Message);

            tcpclient.Close();
            #endregion
        }

    }
}
