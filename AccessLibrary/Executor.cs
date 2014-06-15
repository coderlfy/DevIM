/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 01:47:25
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    public class Executor : IDisposable
    {
        public Expressions Actions = new Expressions();
        public DbConnection Connector = null;

        private DbTransaction DBTransaction = null;
        private DbCommand Commandor = null;
        private DbDataAdapter Selector = null;
        private CommandorFactory commandfactory = null;
        private DbCommandBuilder commandBuilder = null;
        /// <summary>
        /// 初始化Sql库对象组
        /// </summary>
        private void initSql()
        {
            #region
            this.Connector = new SqlConnection();
            this.commandfactory = new AccessLibrary.Sql.CommandSQL();
            #endregion
        }
        /// <summary>
        /// 初始化oracle库对象组
        /// </summary>
        private void initOracle()
        {
            #region
            this.Connector = new OracleConnection();
            this.commandfactory = new AccessLibrary.Oracle.CommandOracle();
            #endregion
        }
        /// <summary>
        /// 初始化全部命令组
        /// </summary>
        private void initAllCommand()
        {
            #region
            this.commandfactory.CreateGeneral(this.Connector, 
                this.Actions.GeneralCommandCount);

            this.commandfactory.CreateAdvance(this.Connector, 
                this.Actions.AdvanceCommandCount);

            this.Commandor = this.commandfactory._Commandor;
            this.Selector = this.commandfactory._Selector;
            this.commandBuilder = this.commandfactory._CommandBuilder;

            #endregion
        }

        /// <summary>
        /// 构造函数（根据数据库类型构建对象组）
        /// </summary>
        /// <param name="DBType"></param>
        public Executor(EnumDB dbType)
        {
            #region
            switch (dbType)
            {
                case EnumDB.SqlServer:
                    this.initSql();
                    break;
                case EnumDB.Oracle:
                    this.initOracle();
                    break;
                default:
                    break;
            }

            #endregion
        }
        /// <summary>
        /// 默认为Sql Server对象组
        /// </summary>
        public Executor()
        {
            #region
            this.initSql();
            #endregion
        }
        /// <summary>
        /// 清理执行器中的action
        /// </summary>
        public void ClearAction()
        {
            #region
            this.Actions.Clear();
            #endregion
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            #region
            Dispose(true);
            GC.SuppressFinalize(true);
            #endregion
        }
        /// <summary>
        /// 释放定义的Command资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            #region
            if (!disposing)
                return;

            if (this.DBTransaction != null)
                this.DBTransaction.Dispose();

            if (this.Commandor != null)
                this.Commandor.Dispose();

            if (this.Selector != null)
                this.Selector.Dispose();

            if (this.commandBuilder != null)
                this.commandBuilder.Dispose();

            if (this.Connector != null)
            {
                this.Connector.Close();
                this.Connector.Dispose();
            }
            if (this.Actions != null)
            {
                this.Actions.Clear();
                this.Actions = null;
            }

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void fillDsByStoredProcedure(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            string tablename = expresser.GetViewTablename(sqlexpression);
            SqlCommand cmd = (SqlCommand)this.Selector.SelectCommand;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SupesoftPage";
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 2000).Value = expresser.CreateSelectSQL(sqlexpression);
            cmd.Parameters.Add("@ReturnFields", SqlDbType.NVarChar, 500).Value = sqlexpression.SqlConditions.ReturnFields;
            cmd.Parameters.Add("@PageSize", SqlDbType.NVarChar, 500).Value = sqlexpression.SqlConditions.PageSize;
            cmd.Parameters.Add("@PageIndex", SqlDbType.NVarChar, 500).Value = sqlexpression.SqlConditions.PageIndex;
            cmd.Parameters.Add("@Where", SqlDbType.NVarChar, 500).Value = "";
            cmd.Parameters.Add("@Orderfld", SqlDbType.NVarChar, 500).Value = "uploadtime";
            cmd.Parameters.Add("@OrderType", SqlDbType.NVarChar, 500).Value = 1;
            this.Selector.Fill(sqlexpression.ReturnDS, tablename);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void fillDsByCustom(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            string tablename = expresser.GetViewTablename(sqlexpression);
            this.Selector.SelectCommand.CommandText = expresser.CreateTopSelectSQL(sqlexpression);
            this.Selector.Fill(sqlexpression.ReturnDS, tablename);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void fillDsWithoutPage(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            string tablename = expresser.GetViewTablename(sqlexpression);
            this.Selector.SelectCommand.CommandText = expresser.CreateSelectSQLWithoutPage(sqlexpression);
            this.Selector.Fill(sqlexpression.ReturnDS, tablename);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void saveDs(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            this.Selector.SelectCommand.CommandText = sqlexpression.SqlBusiness;
            this.Selector.InsertCommand = this.commandBuilder.GetInsertCommand();
            this.Selector.UpdateCommand = this.commandBuilder.GetUpdateCommand();
            this.Selector.DeleteCommand = this.commandBuilder.GetDeleteCommand();

            this.Selector.Update(sqlexpression.WillSaveDs, sqlexpression.WillSaveDs.Tables[0].TableName);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void execScalar(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            this.Commandor.CommandText = expresser.CreateGeneralSelectSQL(sqlexpression);
            sqlexpression.ReturnValue = this.Commandor.ExecuteScalar();
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlexpression"></param>
        private void execNoQuery(Expression sqlexpression)
        {
            #region
            Expresser expresser = new Expresser();
            this.Commandor.CommandText = expresser.CreateGeneralSelectSQL(sqlexpression);
            sqlexpression.ReturnValue = this.Commandor.ExecuteNonQuery();
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        private void execute()
        {
            #region
            //构建SQL语句
            foreach (Expression sqlexpression in this.Actions)
            {
                this.initAllCommand();
                this.bindTransaction();
                switch (sqlexpression.EnumReturn)
                {
                    case EnumDBReturnAccess.Scalar:
                        this.execScalar(sqlexpression);
                        break;
                    case EnumDBReturnAccess.ExeNoQuery:
                        this.execNoQuery(sqlexpression);
                        break;
                    case EnumDBReturnAccess.FillDsByStoredProcedure:
                        this.fillDsByStoredProcedure(sqlexpression);
                        break;
                    case EnumDBReturnAccess.FillDsByCustom:
                        this.fillDsByCustom(sqlexpression);
                        break;
                    case EnumDBReturnAccess.FillDsWithoutPage:
                        this.fillDsWithoutPage(sqlexpression);
                        break;
                    case EnumDBReturnAccess.SaveDS:
                        this.saveDs(sqlexpression);
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        /// <summary>
        /// 数据库连接打开
        /// </summary>
        private void connectOpen()
        {
            #region
            if (string.IsNullOrEmpty(this.Connector.ConnectionString))
                this.Connector.ConnectionString = ConnectString.getDefault();

            if (this.Connector.State == System.Data.ConnectionState.Closed)
                this.Connector.Open();

            #endregion
        }
        /// <summary>
        /// 是否开启事务
        /// </summary>
        /// <returns></returns>
        private bool isTransaction()
        {
            #region
            return (this.Actions.Count > 1);
            #endregion
        }
        /// <summary>
        /// 绑定事务
        /// </summary>
        private void bindTransaction()
        {
            #region

            if (this.Actions.GeneralCommandCount > 0)
                this.Commandor.Transaction = this.DBTransaction;

            if (this.Actions.AdvanceCommandCount > 0)
            {
                this.Selector.SelectCommand.Transaction = this.DBTransaction;
                this.Selector.UpdateCommand.Transaction = this.DBTransaction;
                this.Selector.DeleteCommand.Transaction = this.DBTransaction;
                this.Selector.InsertCommand.Transaction = this.DBTransaction;
            }

            #endregion
        }
        /// <summary>
        /// 开始执行
        /// </summary>
        public Expressions Start()
        {
            #region
            
            bool opentransaction = this.isTransaction();
            try
            {
                this.connectOpen();

                if (opentransaction)
                {
                    this.DBTransaction = this.Connector.BeginTransaction();

                    

                    this.execute();

                    this.DBTransaction.Commit();
                }
                else
                    this.execute();
                return (Expressions)this.Actions.Clone();
            }
            catch (Exception err)
            {
                if (opentransaction)
                    this.DBTransaction.Rollback();
                throw new ApplicationException(err.Message);
            }
            #endregion
        }
    }
}
