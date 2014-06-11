using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    class ProtocolRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orginaldata"></param>
        /// <returns></returns>
        public static byte[] CreatePipeCommand(
            ISocketOrginalData orginaldata)
        {
            return orginaldata.GetCommand();
            //添加构建头尾命令字等
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeData"></param>
        /// <returns></returns>
        public static ISocketOrginalData GetTProtocol(
            List<byte> pipeData)
        {
            #region
            ISocketOrginalData orgdata = null;
            if (verify(pipeData))
                orgdata = new RecvSendFileAck();
            else
                orgdata = new NoneCommand();
            orgdata._AfterDecodeData = decode(pipeData);
            return orgdata;
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
