/****************************************
###创建人：bhlfy
###创建时间：2011-09-14
###公司：ICat科技
###最后修改时间：2011-09-14
###最后修改人：bhlfy
###修改摘要：
****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DevIM.socket
{
    class ErrorEventArgs : EventArgs
    {
        private Exception exception;

        public Exception SocketException
        {
            get { return exception; }
            set { exception = value; }
        }
        
    }
    class TcpServer
    {
        /// <summary>
        /// 服务器端的监听器
        /// </summary>
        private Socket _tcpServer = null;
        /// <summary>
        /// 保存下发指令（字节数组）
        /// </summary>
        private byte[] _recvDataBuffer = new byte[2048];
        /// <summary>
        /// 同步执行插入客户端列表锁
        /// </summary>
        private readonly static object InsertClientLock = true;
        /// <summary>
        /// 
        /// </summary>
        private readonly static object WriteErrorLogLock = true;
        /// <summary>
        /// 
        /// </summary>
        public delegate void ErrorHandler(object sender, ErrorEventArgs e);
        public event ErrorHandler OnError = null;
        /// <summary>
        /// 获取服务端IP列表
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] GetServerIpList()
        {
            #region
            IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
            return ip;
            #endregion
        }
        private static string GetServerIpString()
        {
            #region
            string ipstring = "";
            foreach (IPAddress ip in GetServerIpList())
                ipstring += "\r\n" + ip.ToString();
            return ipstring;
            #endregion
        }
        private void writeError(Exception e)
        {
            #region
            lock (WriteErrorLogLock)
            {
                if (OnError != null)
                    OnError(this, new ErrorEventArgs { SocketException = e });
            }
            #endregion
        }
        /// <summary>
        /// 开始侦听，侦听成功才开启接收
        /// </summary>
        /// <param name="socketResult">socket操作结果</param>
        /// <returns>侦听是否成功</returns>
        public bool StartListen(int serverPort)
        {
            #region
            try
            {
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, serverPort);
                _tcpServer = new Socket(localEP.Address.AddressFamily, 
                    SocketType.Stream, ProtocolType.Tcp);
                _tcpServer.Bind(localEP);
                _tcpServer.Listen(100);
                return true;
            }
            catch (Exception e)
            {
                this.writeError(e);
                return false;
            }
            #endregion
        }
        /// <summary>
        /// 从终端接收信息接口
        /// </summary>
        public void ReceiveSocket()
        {
            #region
            try
            {
                _tcpServer.BeginAccept(new AsyncCallback(acceptConn), 
                    _tcpServer);
            }
            catch (Exception e)
            {
                this.writeError(e);
            }
            #endregion
        }
        /// <summary>
        /// 接收连接（异步阻塞式）
        /// </summary>
        /// <param name="iar"></param>
        private void acceptConn(IAsyncResult iar)
        {
            #region
            try
            {
                Socket oldserver = (Socket)iar.AsyncState;
                Socket client = oldserver.EndAccept(iar);

                _tcpServer.BeginAccept(new AsyncCallback(acceptConn), 
                    _tcpServer);

                client.BeginReceive(_recvDataBuffer, 0, 
                    _recvDataBuffer.Length, SocketFlags.None,
                            new AsyncCallback(receiveData), client);
            }
            catch (SocketException e)
            {
                this.ReceiveSocket();
            }
            catch (Exception e)
            {
                this.writeError(e);
            }
            #endregion
        }
        /// <summary>
        /// 从客户端接收信息
        /// </summary>
        /// <param name="iar"></param>
        private void receiveData(IAsyncResult iar)
        {
            #region
            try
            {
                Socket client = (Socket)iar.AsyncState;
                IPEndPoint endremotepoint = (System.Net.IPEndPoint)client.RemoteEndPoint;
                IPAddress clientIp = endremotepoint.Address;
                int clientport = endremotepoint.Port;

                byte[] businessdata;
                string hexbusinessdata = "";
                int recvcount = client.EndReceive(iar);

                if (recvcount <= 0)
                {
                    client.Close();
                    return;
                }
                //获取接收到的业务数据数组
                businessdata = new byte[recvcount];
                Buffer.BlockCopy(_recvDataBuffer, 0, businessdata, 0, recvcount);

                //构造显示数据
                for (int i = 0; i < recvcount; i++)
                {
                    hexbusinessdata += businessdata[i].ToString("X2"); //ToString("X2")将接收到的byte型数组解包为Unicode字符串
                    hexbusinessdata += " ";
                }
                string[] viewArr = new string[3];
                viewArr[0] = DateTime.Now.ToString();
                viewArr[1] = clientIp.ToString();
                viewArr[2] = System.Text.Encoding.ASCII.GetString(businessdata);//hexbusinessdata;

                Console.WriteLine(hexbusinessdata);
                //MainView.AsyncAppendContent(String.Format("[{0}]接受自终端{1}的信息----{2}\r\n\r\n", viewArr));
                //工厂处理数据（处理过程中维护终端列表）
                //CommandFactory resolvecmd = new CommandFactory(businessdata, viewArr[1], clientport.ToString());
                //resolvecmd.SaveCommand();
                //最终显示终端列表
            }
            catch (Exception e)
            {
                this.writeError(e);
            }
            #endregion
        }
        /// <summary>
        /// 插入分站编号列表
        /// </summary>
        /// <param name="stationNO"></param>
        /*
        public static void InsertClientList(string stationNO)
        {
            #region
            lock (InsertClientLock)
            {
                if (_clientList.Contains(stationNO))
                {
                    _clientList[stationNO] = DateTime.Now;
                    return;
                }
                _clientList.Add(stationNO, DateTime.Now);
            }
            #endregion
        }
         * */
    }
}
