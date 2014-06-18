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

namespace DevIM
{
    public partial class Logon : Form
    {
        public Logon()
        {
            InitializeComponent();
        }

        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            #region
            //验证UI接收输入是否正确？？
            //开启登录时动态变化UI？？
            //此处要异步开启登录请求
            MethodInvoker gd = new MethodInvoker(UserLogonRequest);
            //异步请求中传入callback方法控制变化UI的最终结果？
            //其实上述方法应该返回bool类型，以确定返回登录是否成功。
            gd.BeginInvoke(null, null);
            #endregion
        }

        public void UserLogonRequest()
        {
            #region

            TcpClient tcpclient = new TcpClient(
                this.tbServerIP.Text, int.Parse(this.tbServerPort.Text));

            SendUserValidCheck senduservalidcheck = new SendUserValidCheck()
            {
                _UserInfor = new EntityTUser()
                {
                    userid = this.tbUserName.Text,
                    userpwd = this.tbUserPwd.Text
                }
            };

            byte[] command = senduservalidcheck.GetProtocolCommand();

            //下述5行代码可复用，用来显示发送指令是否预期
            StringBuilder viewcontent = new StringBuilder();
            for (int i = 0; i < command.Length; i++)
            {
                byte temp = command[i];
                viewcontent.Append(string.Format("0x{0} ", temp.ToString("X2")));
            }

            tcpclient.Connect();

            tcpclient.SendToEndDevice(command);

            tcpclient.Receive();

            RecvUserCheckResult usercheckresult = new RecvUserCheckResult();

            tcpclient.Dispatcher(usercheckresult);

            Console.WriteLine(usercheckresult._Result._Message);

            tcpclient.Close();
            #endregion
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            #region
            WinIconStatus iconStatus = new WinIconStatus("appclient.ico");
            iconStatus.BindToWindow(this);
            #endregion
        }

    }
}
