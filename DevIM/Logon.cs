using DevIM.Model;
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

        private WinIconStatus _iconStatus = null;

        private static EntityTUser _user;

        public static EntityTUser _User
        {
            get { return _user; }
            set { _user = value; }
        }
        
        
        private void btnUserLogin_Click(object sender, EventArgs e)
        {
            #region
            //验证UI接收输入是否正确？？
            //开启登录时动态变化UI？？
            //此处要异步开启登录请求
            CheckFormatValid();

            DLUserCheck gd = new DLUserCheck(UserLogonRequest);
            //异步请求中传入callback方法控制变化UI的最终结果？
            //其实上述方法应该返回bool类型，以确定返回登录是否成功。
            AsyncCallback callbak = new AsyncCallback(AfterUserCheck);
            gd.BeginInvoke(callbak, gd);
            //登录成功则本窗口隐藏，主视图为UserMainWindow

            
            #endregion
        }

        private delegate bool DLUserCheck();

        private void AfterUserCheck(IAsyncResult result)
        {
            #region
            DLUserCheck dl_do = (DLUserCheck)result.AsyncState;
            
            if(!this.InvokeRequired)
                return;

            if(dl_do.EndInvoke(result))
                this.Invoke(new MethodInvoker(() => {
                    _iconStatus._systemNotifyIcon.Text += "\r\nNO：" + _User.userid;
                    this.Hide();
                    UserMainWindow mainwindow = new UserMainWindow();
                    mainwindow._IconController = new icon.IconController();
                    mainwindow._IconController.Bind(_iconStatus);
                    mainwindow.Show();
                    _iconStatus.BindToWindow(mainwindow);
                }));
            else
                this.Invoke(new MethodInvoker(() =>
                {
                    ExtMessage.Show("帐号异常！");
                }));
            #endregion
        }
        private bool CheckFormatValid()
        {
            #region
            object serverip, serverport;

            serverip = this.tbServerIP.Text;
            serverport = this.tbServerPort.Text;
            _User.userid = this.tbUserName.Text;
            _User.userpwd = this.tbUserPwd.Text;

            Config.Update(ServerInfor.KeyNameServerIP, ref serverip);
            Config.Update(ServerInfor.KeyNameServerPort, ref serverport);

            ServerInfor._Ip = serverip;
            ServerInfor._Port = Convert.ToInt16(serverport);
            return true;
            #endregion
        }

        public bool UserLogonRequest()
        {
            #region

            TcpClientEx tcpclient = new TcpClientEx(
                ServerInfor._Ip.ToString(), Convert.ToInt16(ServerInfor._Port));
            SendUserValidCheck senduservalidcheck = new
                SendUserValidCheck() { _UserInfor = _User };
            byte[] command = senduservalidcheck.GetProtocolCommand();
            tcpclient.Connect();
            tcpclient.SendToEndDevice(command);
            tcpclient.Receive();
            RecvUserCheckResult usercheckresult = new RecvUserCheckResult();
            tcpclient.Dispatcher(usercheckresult);
            tcpclient.Close();

            if (usercheckresult._Result._Success)
            {
                string message = usercheckresult._Result._Message;
                int splitindex = message.IndexOf("-");
                _User.uid = message.Substring(0, splitindex);
                _User.userfullName = message.Substring(splitindex + 1, message.Length - splitindex - 1);
            }
            return usercheckresult._Result._Success;
            #endregion
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            #region
            _iconStatus = new WinIconStatus("appclient.ico");
            _iconStatus.BindToWindow(this);

            _User = new EntityTUser();


            //封装？
            this.tbServerIP.Text = ServerInfor._Ip.ToString();
            this.tbServerPort.Text = ServerInfor._Port.ToString();

            //DevIM.icon.IconCollector.Init();
            //(new DevIM.icon.IconController()).Bind(_iconStatus);
            #endregion
        }

    }
}
