using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class ProtocolRule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orginaldata"></param>
        /// <returns></returns>
        public static byte[] CreatePipeCommand(
            ISocketCommand orginaldata)
        {
            #region
            List<byte> businesscommand = orginaldata.GetCommand();
            businesscommand.Insert(0, (byte)TProtocol.Head);
            businesscommand.Add((byte)TProtocol.Tail);
            return businesscommand.ToArray<byte>();
            //添加构建头尾命令字等
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeData"></param>
        /// <returns></returns>
        public static ISocketCommand GetTProtocol(
            List<byte> pipeData)
        {
            #region

            if (!verify(pipeData))
                return null;
            else
                return GetTProtocol((TProtocol)pipeData[1]);
            #endregion
        }


        public static ISocketCommand GetTProtocol(
            TProtocol tprotocol)
        {
            #region
            ISocketCommand orgdata = null;
            switch (tprotocol)
            { 
                case TProtocol.SendFileAck:
                    orgdata = new SendFileAck();
                    break;
                case TProtocol.SendFileSyn:
                    orgdata = new SendFileSyn();
                    break;
                default:
                    orgdata = new NoneCommand();
                    break;
            }
            return orgdata;
                    /*
            ISocketCommand orgdata = null;
            if (verify(pipeData))
                orgdata = new SendFileAck();
            else
                orgdata = new NoneCommand();
            orgdata._AfterDecodeData = decode(pipeData);
            return orgdata;
                     * */
            #endregion
        }

        /// <summary>
        /// 验证数据格式
        /// </summary>
        /// <returns></returns>
        private static bool verify(
            List<byte> pipeData)
        {
            #region
            return true;
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeData"></param>
        /// <returns></returns>
        private static List<byte> decode(
            List<byte> pipeData)
        {
            #region
            return new List<byte>();
            #endregion
        }
    }
}
