using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace DevIM.socket
{
    class TcpClient
    {
        private IPAddress destIpAddress = null;
        private int destMachinePort = 1005;
        public TcpClient(string ipAddr, int port) {
            destIpAddress = IPAddress.Parse(ipAddr);
            destMachinePort = port;
        }

        public void SendToEndDevice(byte[] data)
        {
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ServerInfo = new IPEndPoint(this.destIpAddress, this.destMachinePort);
            try
            {
                ClientSocket.Connect(ServerInfo);
                ClientSocket.Send(data);
            }
            catch
            {
            }
            finally
            {
                ClientSocket.Close();
                ClientSocket.Dispose();
            }
        }
    }
}
