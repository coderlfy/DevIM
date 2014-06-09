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

namespace IMServer.socket
{

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
        private Thread _thdReceive = null;
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

                _thdReceive = new Thread(new ThreadStart(receiveSocket));
                _thdReceive.IsBackground = true;
                _thdReceive.Start();
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
        private void receiveSocket()
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
            catch (SocketException)
            {
                this.receiveSocket();
            }
            catch (Exception e)
            {
                this.writeError(e);
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="cacheLength"></param>
        private void dispatcher(Socket client, int cacheLength)
        {
            #region
            byte[] temp = new byte[cacheLength];
            Buffer.BlockCopy(_recvDataBuffer, 0, temp, 0, cacheLength);
            IPEndPoint endremotepoint = (System.Net.IPEndPoint)client.RemoteEndPoint;

            TcpDispatcher tcpdispatcher = new TcpDispatcher(client);
            tcpdispatcher._UserData = new UserData
            {
                _SourceData = temp.ToList<byte>(),
                _FromClient = new business.ClientSource {
                    IPAddress = endremotepoint.Address.ToString(),
                    Port = endremotepoint.Port
                }
            };
            viewTempToConsole(tcpdispatcher);
            tcpdispatcher.Run();
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpDispatcher"></param>
        private void viewTempToConsole(TcpDispatcher tcpDispatcher)
        {
            #region
            StringBuilder viewcontent = new StringBuilder();
            
            //构造显示数据
            viewcontent.AppendLine(string.Format(
                "数据来自客户端：{0}, 端口：{1}",
                tcpDispatcher._UserData._FromClient.IPAddress,
                tcpDispatcher._UserData._FromClient.Port
                )); 

            for (int i = 0; i < tcpDispatcher._UserData._SourceData.Count; i++)
            {
                byte temp = tcpDispatcher._UserData._SourceData[i];
                viewcontent.Append(string.Format("0x{0} ",temp.ToString("X2"))); 
            }
            Console.WriteLine(viewcontent);
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
                
                int recvcount = client.EndReceive(iar);

                /*
                if (recvcount <= 0)
                {
                    client.Close();
                    return;
                }
                */
                this.dispatcher(client, recvcount);
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
