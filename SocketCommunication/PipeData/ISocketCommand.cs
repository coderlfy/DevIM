using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketCommunication.PipeData
{
    public abstract class ISocketCommand
    {


        private List<byte> _afterDecodeData;

        public List<byte> _AfterDecodeData
        {
            get { return _afterDecodeData; }
            set { _afterDecodeData = value; }
        }
        

        public abstract bool Analysis();

        public abstract List<byte> GetCommand();

        public byte[] GetProtocolCommand()
        {
            return ProtocolRule.CreatePipeCommand(this);
        }

        public List<string> Split(int itemCount)
        {
            #region
            List<string> analysisinfor = new List<string>();
            string decode = UTF8Encoding.UTF8.GetString(this._AfterDecodeData.ToArray<byte>());

            int startIndex = 0, findIndex = -1;
            //此处的2可为动态
            for (int i = 0; i < itemCount; i++)
            {
                findIndex = decode.IndexOf(";", startIndex);
                if (findIndex != -1)
                {
                    analysisinfor.Add(decode.Substring(startIndex, findIndex - startIndex));
                    startIndex = findIndex + 1;
                }
                else
                {
                    findIndex = decode.Length;
                    analysisinfor.Add(decode.Substring(startIndex, findIndex - startIndex));
                }

            }
            return analysisinfor;
            #endregion
        }

    }
}
