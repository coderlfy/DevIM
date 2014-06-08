using IMServer.business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace IMServer.socket
{
    class UserData
    {
        private ClientSource source;

        public ClientSource _FromClient
        {
            get { return source; }
            set { source = value; }
        }

        private List<byte> sourceData;

        public List<byte> _SourceData
        {
            get { return sourceData; }
            set { sourceData = value; }
        }
        
        
        private List<byte[]> dispatcherData;

        public List<byte[]> _DispatcherData
        {
            get { return dispatcherData; }
            set { dispatcherData = value; }
        }


    }
    class TcpDispatcher
    {
        private UserData userData;
        private Socket _clientSocket = null;

        public UserData _UserData
        {
            get { return userData; }
            set { userData = value; }
        }
        
        public TcpDispatcher(Socket clientSocket)
        {
            this._clientSocket = clientSocket;
        }
        /// <summary>
        /// 公开接口进行批处理
        /// </summary>
        public void Run()
        {
            if (userData._SourceData[1] == 0x10)
            {
                this._clientSocket.Send(new byte[] { 0x03, 0x11, 0x13 });
            }
        }
        /// <summary>
        /// 验证数据格式
        /// </summary>
        /// <returns></returns>
        private bool verify()
        {
            return true;
        }

        private void split()
        { 
            
        }
    }
}
