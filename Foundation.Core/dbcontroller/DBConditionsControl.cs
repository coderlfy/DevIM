/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 01:47:25
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public partial class DBConditions
    {
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldType"></param>
        /// <param name="conditionType">条件类型（>,=,in等枚举类型）</param>
        /// <param name="paramValue">参数传来的值</param>
        /// <param name="conditionsRelation"></param>
        /// <returns></returns>
        private DBConditions add(string fieldName, EnumSqlType fieldType, 
            EnumCondition conditionType, object paramValue, 
            EnumConditionsRelation conditionsRelation)
        {
            #region
            DataRow dr = this.Tables[0].NewRow();
            object maxconditionid = this.Tables[0].Compute("Max(conditionId)", "true");

            dr[DBConditions.conditionId] = (maxconditionid == System.DBNull.Value) ? 1 : Convert.ToInt16(maxconditionid) + 1;
            dr[DBConditions.paramsName] = fieldName;
            dr[DBConditions.fieldName] = fieldName;
            dr[DBConditions.fieldType] = fieldType;
            dr[DBConditions.conditionsRelation] = conditionsRelation;
            dr[DBConditions.enumCondation] = conditionType;
            dr[DBConditions.paramValue] = paramValue;

            this.Tables[0].Rows.Add(dr);
            return this;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="conditionType"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public DBConditions Add(string fieldName, EnumSqlType fieldType, 
            EnumCondition conditionType, object paramValue)
        {
            #region
            return this.add(fieldName, fieldType, conditionType, paramValue, EnumConditionsRelation.And);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldType"></param>
        /// <param name="conditionType"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public DBConditions Or(string fieldName, EnumSqlType fieldType, 
            EnumCondition conditionType, object paramValue)
        {
            #region
            return this.add(fieldName, fieldType, conditionType, paramValue, EnumConditionsRelation.Or);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public DBConditions AddKeys(string keyname)
        {
            #region
            if (this.DataSetKeys == null)
                this.DataSetKeys = new List<string>();

            this.DataSetKeys.Add(keyname);
            return this;
            #endregion
        }
    }
}
