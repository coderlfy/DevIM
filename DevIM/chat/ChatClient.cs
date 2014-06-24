﻿using DevIM.Model;
using DevIMDataLibrary;
using SocketCommunication.PipeData;
using SocketCommunication.TcpSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.chat
{
    class ReceiveEventArgs : EventArgs
    {
        private List<ChatContent> _contents;

        public List<ChatContent> _Contents
        {
            get { return _contents; }
            set { _contents = value; }
        }
        

    }
    class ChatClient
    {
        public event EventHandler<ReceiveEventArgs> OnReceive = null;
        public static TcpClientEx ConnectServer()
        {
            #region
            TcpClientEx tcpclient = new TcpClientEx(
ServerInfor._Ip.ToString(), Convert.ToInt16(ServerInfor._Port));

            tcpclient.Connect();

            return tcpclient;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="destUserId"></param>
        public void Send(string content, int destUserId)
        { 
            #region
            TcpClientEx tcpclient = ChatClient.ConnectServer();

            ChatContent contentobj = new ChatContent() { 
                _FromUID = int.Parse(Logon._User.uid),
                _Text = content,
                _ToUId = destUserId
            };

            SendChatContent sendchatcontent = new SendChatContent() { _Content = contentobj };

            byte[] command = sendchatcontent.GetProtocolCommand();

            tcpclient.SendToEndDevice(command);

            //可接收是否发送成功
            /*
            tcpclient.Receive();

            RecvUserCheckResult usercheckresult = new RecvUserCheckResult();

            tcpclient.Dispatcher(usercheckresult);
            */
            tcpclient.Close();

            #endregion
        }

        public void RegisterListen()
        {
            #region
            TcpClientEx tcpclient = ChatClient.ConnectServer();

            SendRegisterClientListen sendregister = new SendRegisterClientListen() { _UserInfor = Logon._User };

            byte[] command = sendregister.GetProtocolCommand();

            tcpclient.SendToEndDevice(command);

            //可接收是否发送成功
            while(true)
            { 
                tcpclient.Receive();
                RecvChatContent chatcontent = new RecvChatContent();
                tcpclient.Dispatcher(chatcontent);

                Console.WriteLine("来源:{0},内容是：{1}", 
                    chatcontent._Content._FromUID, 
                    chatcontent._Content._Text);
            }
            #endregion
        }
    }
}