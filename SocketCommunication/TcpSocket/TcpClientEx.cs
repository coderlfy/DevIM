using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SocketCommunication.PipeData;
using System.IO;

namespace SocketCommunication.TcpSocket
{
    public class TcpClientEx
    {
        
        private IPAddress destIpAddress = null;

        private int destMachinePort = 1005;

        private TcpClient _clientSocket = null;

        private byte[] _RecvBytes = new byte[1024];

        private static List<byte> _fullrecvdata = null;

        private Stream _pipeStream = null;

        public TcpClientEx(string ipAddr, int port)
        {
            destIpAddress = IPAddress.Parse(ipAddr);
            destMachinePort = port;
        }

        public void Connect()
        {
            #region
            _clientSocket = new TcpClient();
            IPEndPoint ServerInfo = new IPEndPoint(this.destIpAddress, this.destMachinePort);
            //_clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 30000);
            _clientSocket.Connect(ServerInfo);
            _pipeStream = this._clientSocket.GetStream();

            #endregion
        }

        public void SendToEndDevice(byte[] data)
        {
            try
            {
                _pipeStream.Write(data, 0, data.Length);
            }
            catch
            {
            }
        }
        public void ReceiveFile()
        {
            try
            {
                StreamWriter OnlineInf = new StreamWriter("online.dat");
                int k = _pipeStream.Read(_RecvBytes, 0, _RecvBytes.Length);
                while (k > 0)
                {
                    string Str = System.Text.UTF8Encoding.UTF8.GetString(_RecvBytes, 0, k);
                    OnlineInf.Write(Str);
                    k = _pipeStream.Read(_RecvBytes, 0, _RecvBytes.Length);
                }

                OnlineInf.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
            }
        }
        public void Receive()
        {
            #region
            _fullrecvdata = new List<byte>();
            while (true)
            {
                Int32 bytes = _pipeStream.Read(_RecvBytes, 0, _RecvBytes.Length);
                int index0x13 = -1;

                if (bytes > 0)
                {

                    for (int i = 0; i < bytes; i++)
                    {
                        _fullrecvdata.Add(_RecvBytes[i]);
                        if (_RecvBytes[i] == 0x13)
                        {
                            index0x13 = i;
                            break;
                        }
                    }
                }
                if (bytes == 0)
                    break;

                if (index0x13 > -1)
                    if (_RecvBytes[index0x13] == 0x13)
                        break;
            }


            #endregion
        }
        public TProtocol GetResolveType()
        {
            return (TProtocol)_fullrecvdata[1];
        }

        public void Dispatcher(IClientCommand command)
        {
            //与实际接收到的_fullrecvdata信息做对比
            TcpClientDispatcher clientdispatcher = new TcpClientDispatcher(command);
            //漏洞：_fullrecvdata有可能已经过滤了有效数据，两帧数据合并的现象
            List<byte> fullrecvdata = _fullrecvdata.ToList<byte>();

            fullrecvdata.RemoveAt(0);
            fullrecvdata.RemoveAt(0);
            fullrecvdata.RemoveAt(fullrecvdata.Count-1);
            command._AfterDecodeData = fullrecvdata;
            //command//此处添加属性
            clientdispatcher.Run();
        }

        public void Close()
        {
            if (_clientSocket != null)
            {
                _clientSocket.Client.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
                //_clientSocket.Dispose();
            }

        }
    }
}
