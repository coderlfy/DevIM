﻿using SocketCommunication.Cache;
using SocketCommunication.PipeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SocketCommunication.TcpSocket
{
    public class TcpOnlineListener
    {
        private Thread _thdOnlineResolve = null;

        public void Start()
        {

            byte[] data = (new RecvOnlineMarkup()).GetProtocolCommand();
            _thdOnlineResolve = new Thread(new ThreadStart(() =>
            {

                while (true)
                {
                    foreach (Customer customer in
                        CustomerCollector._Customers)
                    {
                        customer._SrcSocket.Send(data);
                        /*
                        customer._SrcSocket.BeginSend(data, 0, data.Length,
                            System.Net.Sockets.SocketFlags.None, null, null);
                         * */
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(30));
                }

            }));

            _thdOnlineResolve.IsBackground = true;
            _thdOnlineResolve.Start();

        }
    }
}
