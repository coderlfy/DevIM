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
    public abstract class IDBAccess : IDisposable
    {
        public Executor _executor = null;
        public Expressions Actions = null;

        protected EnumDB _dbType = EnumDB.SqlServer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        public abstract void SetConnectString(string fullString);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyname"></param>
        public abstract void SetConnectStringByKey(string keyname);
        /// <summary>
        /// 添加update、insert、delete操作,带过滤条件
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="enumReturn"></param>
        /// <param name="conditions"></param>
        public abstract void AddAction(string sqlExpression, 
            EnumDBReturnAccess enumReturn, DBConditions conditions);
        /// <summary>
        /// 添加update、insert、delete操作,不带过滤条件
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="enumReturn"></param>
        public abstract void AddAction(string sqlExpression, 
            EnumDBReturnAccess enumReturn);
        /// <summary>
        /// 添加对数据集的保存操作
        /// </summary>
        /// <param name="willSaveDs"></param>
        public abstract void AddAction(DataSet willSaveDs);
        /// <summary>
        /// 添加检索操作，带条件过滤
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="conditions"></param>
        /// <param name="fillDs"></param>
        /// <param name="enumReturn"></param>
        public abstract void AddAction(string sqlExpression,
            DBConditions conditions, DataSet fillDs, 
            EnumDBReturnAccess enumReturn);
        /// <summary>
        /// 添加检索操作，不带过滤条件
        /// </summary>
        /// <param name="sqlExpression"></param>
        /// <param name="fillDs"></param>
        /// <param name="enumReturn"></param>
        public abstract void AddAction(string sqlExpression,
            DataSet fillDs, EnumDBReturnAccess enumReturn);
        /// <summary>
        /// 节目开始
        /// </summary>
        public abstract void StartActions();
        /// <summary>
        /// 清理节目
        /// </summary>
        public abstract void ClearActions();
        /// <summary>
        /// 销毁对象
        /// </summary>
        public virtual void Dispose()
        {
            #region
            if (this.Actions != null)
                this.Actions.Clear();
            
            this._executor.Dispose();
            #endregion
        }
    }
}
