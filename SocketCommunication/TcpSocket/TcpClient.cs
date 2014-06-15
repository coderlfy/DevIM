﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SocketCommunication.PipeData;

namespace SocketCommunication.TcpSocket
{
    public class TcpClient
    {
        private IPAddress destIpAddress = null;

        private int destMachinePort = 1005;

        private Socket _clientSocket = null;

        private byte[] _RecvBytes = new byte[1024];

        private static List<byte> _fullrecvdata = null;

        public TcpClient(string ipAddr, int port)
        {
            destIpAddress = IPAddress.Parse(ipAddr);
            destMachinePort = port;
        }

        public void Connect()
        {
            #region
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerInfo = new IPEndPoint(this.destIpAddress, this.destMachinePort);
            _clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 30000);
            _clientSocket.Connect(ServerInfo);
            #endregion
        }

        public void SendToEndDevice(byte[] data)
        {
            try
            {
                _clientSocket.Send(data);
            }
            catch
            {
            }
        }

        public void receive()
        {
            #region
            _fullrecvdata = new List<byte>();
            while (true)
            {
                Int32 bytes = _clientSocket.Receive(_RecvBytes, _RecvBytes.Length, SocketFlags.None);
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
        public void Dispatcher(IClientCommand command)
        {
            //与实际接收到的_fullrecvdata信息做对比
            new TcpClientDispatcher(command).Run();
        }

        public void Close()
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
                _clientSocket.Dispose();
            }

        }
    }
}
