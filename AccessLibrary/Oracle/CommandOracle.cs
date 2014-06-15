/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
***公司：山西博华科技有限公司
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace AccessLibrary.Oracle
{
    public class CommandOracle : CommandorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connection"></param>
        /// <param name="commandCount"></param>
        /// <returns></returns>
        public override void CreateGeneral(DbConnection _connection, 
            int commandCount)
        {
            #region
            if (commandCount > 0)
            {
                this._Commandor = new OracleCommand();
                this._Commandor.Connection = _connection;
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_connection"></param>
        /// <param name="advanceCommandCount"></param>
        /// <returns></returns>
        public override void CreateAdvance(DbConnection _connection, 
            int advanceCommandCount)
        {
            #region
            if (advanceCommandCount > 0)
            {

                OracleDataAdapter dataadapter = new OracleDataAdapter();
                base._Selector = dataadapter;
                base._Selector.SelectCommand = new OracleCommand();
                base._Selector.DeleteCommand = new OracleCommand();
                base._Selector.InsertCommand = new OracleCommand();
                base._Selector.UpdateCommand = new OracleCommand();
                base._Selector.SelectCommand.Connection = _connection;
                base._Selector.DeleteCommand.Connection = _connection;
                base._Selector.InsertCommand.Connection = _connection;
                base._Selector.UpdateCommand.Connection = _connection;

                base._CommandBuilder = new OracleCommandBuilder(dataadapter);
            }
            #endregion
        }
    }
}
