using DevIM.icon;
using DevIM.Model;
using DevIMDataLibrary;
using SocketCommunication.PipeData;
using SocketCommunication.TcpSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevIM.chat
{
    class ChatClient
    {
        //public event EventHandler<ReceiveEventArgs> OnReceive = null;
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
        /// <summary>
        /// 
        /// </summary>
        public void RegisterListen(IconController iconController)
        {
            #region

            TcpClientEx tcpclient = ChatClient.ConnectServer();

            SendRegisterClientListen sendregister = new SendRegisterClientListen() { _UserInfor = Logon._User };
            SendOnlineMarkup sendonlinemarkup = new SendOnlineMarkup() { _UserInfor = Logon._User };

            byte[] sendregistercommand = sendregister.GetProtocolCommand();
            byte[] sendonlinecommand = sendonlinemarkup.GetProtocolCommand();
            tcpclient.SendToEndDevice(sendregistercommand);

            //可接收是否发送成功
            while(true)
            {
                tcpclient.Receive();

                switch (tcpclient.GetResolveType())
                { 
                    case TProtocol.RecvChatContent:
                        RecvChatContent chatcontentcmd = new RecvChatContent();
                        tcpclient.Dispatcher(chatcontentcmd);
                        Console.WriteLine("来源:{0},内容是：{1}",
                    chatcontentcmd._Content._FromUID,
                    chatcontentcmd._Content._Text);

                        Friend friend = new Friend() { 
                            _User = new EntityTUser() { 
                                uid = chatcontentcmd._Content._FromUID.ToString() 
                            } 
                        };

                        int timestartindex = chatcontentcmd._Content._Text.Length - 19;
                        string dt = chatcontentcmd._Content._Text.Substring(timestartindex, 19);

                        ChatMessage message = new ChatMessage() {
                            _Content = chatcontentcmd._Content._Text.Substring(0, timestartindex),
                            _RecvTime = DateTime.Parse(dt)
                        };

                        Friend findfriend = FriendCollector.FindFriend(friend);
                        if (findfriend != null)
                        {
                            if (findfriend._MessageMode == MessageMode.HasPop)
                            {
                                findfriend._Messages.Add(message);
                                TrafficMsg.PostMessage(int.Parse(findfriend._FrmHandle.ToString()), 500, 0, 0);
                            }
                            else
                                addMessageToFriend(findfriend, message);
                        }
                        else
                        {

                            friend._User.userfullName = "得建立缓存";
                            friend._User.userid = "000000000";
                            friend._Messages = new List<ChatMessage>();
                            friend._MessageMode = MessageMode.None;
                            friend._Messages.Add(message);

                            FriendCollector.Add(friend);
                            TrafficMsg.PostMessage(int.Parse(UserMainWindow._FrmHandle.ToString()), 501, 0, 0);
                        }
                            
                        break;
                    case TProtocol.RecvOnlineMarkup:
                        tcpclient.SendToEndDevice(sendonlinecommand);
                        break;
                }
                
            }
            #endregion
        }
        private void addMessageToFriend(Friend findfriend, ChatMessage message)
        {
            #region
            findfriend._MessageMode = MessageMode.None;
            findfriend._Messages.Add(message);
            TrafficMsg.PostMessage(int.Parse(UserMainWindow._FrmHandle.ToString()), 501, 0, 0);
            #endregion
        }
    }
}
