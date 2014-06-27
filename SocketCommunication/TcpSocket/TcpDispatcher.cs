
using SocketCommunication.Cache;
using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.TcpSocket
{

    public abstract class TcpDispatcher
    {
        
        
        /// <summary>
        /// 公开接口进行批处理
        /// </summary>
        public abstract bool Run();

        /// <summary>
        /// 有一帧内有多条命令
        /// </summary>
        protected void split()
        {
            #region
            #endregion
        }
    }
}
