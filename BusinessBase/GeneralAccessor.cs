using AccessLibrary;
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessBase
{
    public class GeneralAccessor
    {
        protected string _connectionString = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbaccess"></param>
        protected void setConnectString(IDBAccess dbaccess)
        {
            #region
            if (!string.IsNullOrEmpty(this._connectionString))
                dbaccess.SetConnectString(this._connectionString);
            #endregion
        }
        /// <summary>
        /// 获取指定分页的数据（一般用于web类型grid获取数据）。
        /// </summary>
        /// <param name="sql">业务关联的sql语句</param>
        /// <param name="sqlcount">获取条件过滤后的所有记录条数</param>
        /// <param name="fillDs">传入的空数据集</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>符合where条件的记录条数（去除分页）</returns>
        protected int GetCustomBusiness(string sql,
            string sqlcount, DataSet fillDs,
            DBConditions conditions)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            int rowscount = 0;
            try
            {
                string tablename = (fillDs.Tables.Count > 0) ?
                    fillDs.Tables[0].TableName : "tablename";

                this.setConnectString(dbaccess);

                dbaccess.AddAction(sql,
                    conditions, fillDs, EnumDBReturnAccess.FillDsByCustom);

                dbaccess.AddAction(sqlcount,
                    EnumDBReturnAccess.Scalar, conditions);

                dbaccess.StartActions();
                rowscount = Convert.ToInt32(dbaccess.Actions[1].ReturnValue);
                dbaccess.ClearActions();

                return rowscount;
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }

        protected void GetWithoutPageBusiness(string sql, DataSet fillDs,
            DBConditions conditions)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            try
            {
                string tablename = (fillDs.Tables.Count > 0) ?
                    fillDs.Tables[0].TableName : "tablename";

                this.setConnectString(dbaccess);

                dbaccess.AddAction(sql,
                    conditions, fillDs, EnumDBReturnAccess.FillDsWithoutPage);

                dbaccess.StartActions();
                dbaccess.ClearActions();
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }

        /// <summary>
        /// 获取指定分页的数据（一般用于web类型grid获取数据）。
        /// </summary>
        /// <param name="sql">业务关联的sql语句</param>
        /// <param name="fillDs">传入的空数据集</param>
        /// <param name="conditions">查询条件</param>
        protected void GetCustomBusiness(string sql,
            DataSet fillDs, DBConditions conditions)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            try
            {
                string tablename = (fillDs.Tables.Count > 0) ?
                    fillDs.Tables[0].TableName : "tablename";

                this.setConnectString(dbaccess);

                dbaccess.AddAction(sql,
                    conditions, fillDs, EnumDBReturnAccess.FillDsByCustom);

                dbaccess.StartActions();
                dbaccess.ClearActions();
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }
        /// <summary>
        /// 获取指定分页的数据（一般用于web类型grid获取数据）。
        /// </summary>
        /// <param name="sql">业务关联的sql语句</param>
        /// <param name="fillDs">传入的空数据集</param>
        protected void GetCustomBusiness(string sql,
            DataSet fillDs)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            try
            {
                string tablename = (fillDs.Tables.Count > 0) ?
                    fillDs.Tables[0].TableName : "tablename";

                this.setConnectString(dbaccess);

                dbaccess.AddAction(sql, fillDs, EnumDBReturnAccess.FillDsByCustom);

                dbaccess.StartActions();
                dbaccess.ClearActions();
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }
        /// <summary>
        /// 获取单表数据集（类内部复用）
        /// </summary>
        /// <param name="fillDs">传入的空数据集</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="returntype">数据库访问返回类型</param>
        /// <param name="isCount">是否需要计算记录条数</param>
        /// <returns>符合where条件的记录条数（去除分页）</returns>
        protected int getCustomDs(DataSet fillDs, 
            DBConditions conditions, EnumDBReturnAccess returntype, bool isCount)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            int rowscount = 0;
            try
            {
                string tablename = (fillDs.Tables.Count > 0) ? 
                    fillDs.Tables[0].TableName : "tablename";
                string sqlformat = "select {0} from [{1}]";

                //this.setConnectString(dbaccess);

                dbaccess.AddAction(string.Format(sqlformat, "*", tablename),
                    conditions, fillDs, returntype);

                if(isCount)
                    dbaccess.AddAction(string.Format(sqlformat, "count(*)", tablename),
                    EnumDBReturnAccess.Scalar, conditions);

                dbaccess.StartActions();
                if (isCount)
                    rowscount = Convert.ToInt32(dbaccess.Actions[1].ReturnValue);
                dbaccess.ClearActions();

                return rowscount;
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }
        /// <summary>
        /// 获取单表数据（非存储过程）
        /// </summary>
        /// <param name="fillDs">传入数据集</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>符合where条件的记录条数（去除分页）</returns>
        public int GetSingleT(DataSet fillDs, 
            DBConditions conditions)
        {
            #region
            if (conditions == null)
                conditions = new DBConditions();
            return getCustomDs(fillDs, conditions, 
                EnumDBReturnAccess.FillDsByCustom, true);
            #endregion
        }
        /// <summary>
        /// 获取单表所有数据
        /// </summary>
        /// <param name="fillDs">传入数据集</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>符合where条件的记录条数（去除分页）</returns>
        public int GetSingleTAll(DataSet fillDs,
            DBConditions conditions)
        {
            #region
            if (conditions == null)
                conditions = new DBConditions();
            conditions.PageSize = int.MaxValue;
            return getCustomDs(fillDs, conditions,
                EnumDBReturnAccess.FillDsByCustom, true);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fillDs"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public void GetSingleTAllWithoutCount(DataSet fillDs,
            DBConditions conditions)
        {
            #region
            if (conditions == null)
                conditions = new DBConditions();
            conditions.PageSize = int.MaxValue;
            getCustomDs(fillDs, conditions,
                EnumDBReturnAccess.FillDsByCustom, false);
            #endregion
        }
        /// <summary>
        /// 获取单表数据（存储过程）
        /// </summary>
        /// <param name="fillDs">传入数据集</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>符合where条件的记录条数（去除分页）</returns>
        protected int GetSingleTBySP(DataSet fillDs, 
            DBConditions conditions)
        {
            #region
            return getCustomDs(fillDs, conditions, 
                EnumDBReturnAccess.FillDsByStoredProcedure, true);
            #endregion
        }
        /// <summary>
        /// 保存单表数据集
        /// </summary>
        /// <param name="willSaveDs">欲保存的数据集</param>
        public void SaveSingleT(DataSet willSaveDs)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            try
            {

                this.setConnectString(dbaccess);

                dbaccess.AddAction(willSaveDs);
                dbaccess.StartActions();
                dbaccess.ClearActions(); 
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }
        /// <summary>
        /// 获取数据表的表名和第一关键字的字段名
        /// </summary>
        /// <param name="destDs"></param>
        /// <param name="tablename"></param>
        /// <param name="keyname"></param>
        protected void gettablekey(DataSet destDs, 
            ref string tablename, ref string keyname)
        {
            #region
            if (destDs.Tables.Count > 0)
            {
                tablename = destDs.Tables[0].TableName;
                if (destDs.Tables[0].PrimaryKey.Length > 0)
                    keyname = destDs.Tables[0].PrimaryKey[0].ColumnName;
                else
                    ExtConsole.Write("目标数据表中没有设定主键！");
            }
            else
                ExtConsole.Write("传入的数据集没有DataTable！");
            #endregion
        }
        /// <summary>
        /// 获取数据表的关键字最大值+1
        /// </summary>
        /// <param name="destDs"></param>
        /// <returns></returns>
        public int GetMaxAddOne(DataSet destDs)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            int valueaddone = 1;
            try
            {
                string tablename = "";
                string keyname = "";
                string sql = "select max({0}) from [{1}]";
                
                gettablekey(destDs, ref tablename, ref keyname);
                sql = string.Format(sql, keyname, tablename);

                this.setConnectString(dbaccess);

                dbaccess.AddAction(sql, EnumDBReturnAccess.Scalar);
                dbaccess.StartActions();
                if (dbaccess.Actions[0].ReturnValue != System.DBNull.Value)
                    valueaddone = Convert.ToInt32(dbaccess.Actions[0].ReturnValue)+1;
                dbaccess.ClearActions();

                return valueaddone;
            }
            finally
            {
                dbaccess.Dispose();
            }
            #endregion
        }
    }
}
