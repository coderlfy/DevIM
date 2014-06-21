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
            MethodInvoker gd = new MethodInvoker(RequestUserList);
            //异步请求中传入callback方法控制变化UI的最终结果？
            //其实上述方法应该返回bool类型，以确定返回登录是否成功。
            gd.BeginInvoke(null, null);
        }

        public void RequestUserList()
        {
            #region

            TcpClientEx tcpclient = new TcpClientEx(
                ServerInfor._Ip, ServerInfor._Port);

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

            //tcpclient.Close();
            #endregion
        }

    }
}
