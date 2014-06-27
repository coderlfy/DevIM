using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.PipeData
{
    public class RecvUserCheckResult : IClientCommand
    {
        private MsgResultModel _result;

        public MsgResultModel _Result
        {
            get { return _result; }
            set { _result = value; }
        }


        public override bool Analysis()
        {
            #region

            List<string> analysisinfor = base.Split(2);
            _Result = new MsgResultModel()
            {
                _Success = (analysisinfor[0].ToLower() == "true"),
                _Message = analysisinfor[1]
            };

            return true;
            #endregion
        }

        public override List<byte> GetCommand()
        {
            List<byte> businesscommand = new List<byte>();
            businesscommand.Add((byte)TProtocol.RecvUserCheckResult);
            string content = string.Format("{0};{1}",
                this._result._Success.ToString(),
                this._result._Message
                );

            businesscommand.AddRange(UTF8Encoding.UTF8.GetBytes(content));
            return businesscommand;
        }

    }
}
