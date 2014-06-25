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
        public static IServerCommand GetServerCommand(
            List<byte> pipeData)
        {
            #region

            if (!verify(pipeData))
                return null;
            else
                return CommandFactory.CreateServerCommandObject(
                    (TProtocol)pipeData[1]);
            #endregion
        }

        public static IClientCommand GetClientCommand(
            List<byte> pipeData)
        {
            #region

            if (!verify(pipeData))
                return null;
            else
                return CommandFactory.CreateClientCommandObject(
                    (TProtocol)pipeData[1]);
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
