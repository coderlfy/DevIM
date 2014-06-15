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
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    public class DBAccess : IDBAccess
    {

        /// <summary>
        /// 根据数据库类型创建sql操作
        /// </summary>
        /// <returns></returns>
        private Expression createSqlAction()
        {
            #region
            Expression sqlaction = null;
            switch (this._dbType)
            {
                case EnumDB.SqlServer:
                    sqlaction = new ExpressionSQL();
                    break;
                case EnumDB.Oracle:
                    sqlaction = new ExpressionOracle();
                    break;
                default:
                    sqlaction = new ExpressionSQL();
                    break;
            }
            return sqlaction;
            #endregion
        }
        /// <summary>
        /// 构造函数传入数据库类型
        /// </summary>
        /// <param name="dbType"></param>
        public DBAccess(EnumDB dbType)
        {
            #region
            this._dbType = dbType;
            _executor = new Executor(dbType);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        public DBAccess()
        {
            #region
            _executor = new Executor();
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        private void resetConnector()
        {
            #region
            if (this._executor.Connector.State == ConnectionState.Open)
                this._executor.Connector.Close();
            #endregion
        }
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connectString"></param>
        public override void SetConnectString(string fullString)
        {
            #region
            this.resetConnector();
            this._executor.Connector.ConnectionString = fullString;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyname"></param>
        public override void SetConnectStringByKey(string keyname)
        {
            #region
            this.resetConnector();
            this._executor.Connector.ConnectionString = ConnectString.getConfig(keyname);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="enumReturn"></param>
        /// <param name="conditions"></param>
        public override void AddAction(string sqlExpression, 
            EnumDBReturnAccess enumReturn, DBConditions conditions)
        {
            #region
            if (enumReturn == EnumDBReturnAccess.SaveDS || 
                enumReturn == EnumDBReturnAccess.FillDsByCustom || 
                enumReturn == EnumDBReturnAccess.FillDsByStoredProcedure)
            {
                ExtConsole.Write("该接口不提供存储数据集或检索数据集功能！");
                return;
            }
            Expression sql = this.createSqlAction();
            sql.SqlBusiness = sqlExpression;
            sql.EnumReturn = enumReturn;
            sql.SqlConditions = conditions;
            this._executor.Actions.Add(sql);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="enumReturn"></param>
        public override void AddAction(string sqlExpression, 
            EnumDBReturnAccess enumReturn)
        {
            #region
            this.AddAction(sqlExpression, enumReturn, null);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="willSaveDs"></param>
        /// <param name="tablename"></param>
        /// <param name="primaryKeyName"></param>
        public override void AddAction(DataSet willSaveDs)
        {
            #region
            Expresser expresser = new Expresser();
            Expression sql = this.createSqlAction();
            sql.SqlBusiness = expresser.CreateBuilderSelect(willSaveDs);
            sql.WillSaveDs = willSaveDs;
            sql.EnumReturn = EnumDBReturnAccess.SaveDS;
            this._executor.Actions.Add(sql);
            #endregion
        }
        /// <summary>
        /// 添加检索操作，带条件过滤
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="conditions"></param>
        /// <param name="fillDs"></param>
        public override void AddAction(string sqlExpression,
            DBConditions conditions, DataSet fillDs, EnumDBReturnAccess enumReturn)
        {
            if (enumReturn == EnumDBReturnAccess.ExeNoQuery ||
                enumReturn == EnumDBReturnAccess.SaveDS ||
                enumReturn == EnumDBReturnAccess.Scalar)
            {
                ExtConsole.Write("该接口只提供填充数据集的功能！");
                return;
            }
            Expression sql = this.createSqlAction();
            sql.ReturnDS = fillDs;
            sql.SqlBusiness = sqlExpression;
            sql.SqlConditions = conditions;
            sql.EnumReturn = enumReturn;
            this._executor.Actions.Add(sql);
        }
        /// <summary>
        /// 添加检索操作，不带过滤条件
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="fillDs"></param>
        public override void AddAction(string sqlExpression,
            DataSet fillDs, EnumDBReturnAccess enumReturn)
        {
            if (enumReturn == EnumDBReturnAccess.ExeNoQuery ||
                enumReturn == EnumDBReturnAccess.SaveDS ||
                enumReturn == EnumDBReturnAccess.Scalar)
            {
                ExtConsole.Write("该接口只提供填充数据集的功能！");
                return;
            }
            Expression sql = this.createSqlAction();
            sql.SqlBusiness = sqlExpression;
            sql.ReturnDS = fillDs;
            sql.EnumReturn = enumReturn;
            this._executor.Actions.Add(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        public override void StartActions()
        {
            #region
            base.Actions = this._executor.Start();
            
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        public override void ClearActions()
        {
            #region
            this._executor.Actions.Clear();
            #endregion
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
