/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
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
    public partial class DBConditions : DataSet
    {
        //不只是查询条件、还有排序等相关SQL查询时的各项条件
        public string ReturnFields = null;
        public int PageIndex = 1;
        public int PageSize = int.MaxValue;
        public PageSortCollection PageSorts = null;
        public List<string> DataSetKeys = null;

        #region 常量字符串标识字段名称
        /// <summary>
        /// 查询条件编号。
        /// </summary>
        public const string conditionId = "conditionId";
        /// <summary>
        /// 参数名。
        /// </summary>
        public const string paramsName = "paramsName";
        /// <summary>
        /// 字段名。
        /// </summary>
        public const string fieldName = "fieldName";
        /// <summary>
        /// 对应查询值
        /// </summary>
        public const string paramValue = "paramValue";
        /// <summary>
        /// 条件与条件间的关系
        /// </summary>
        public const string conditionsRelation = "conditionsRelation";
        /// <summary>
        /// 查询方式。
        /// </summary>
        public const string enumCondation = "enumCondation";
        /// <summary>
        /// 字段类型。
        /// </summary>
        public const string fieldType = "fieldType";
        /// <summary>
        /// 表名。
        /// </summary>
        public const string TConditions = "TConditions";
        #endregion

        private void BuildData()
        {
            DataTable dt = new DataTable(TConditions);
            dt.Columns.Add(conditionId, typeof(System.Int32));
            dt.Columns.Add(paramsName, typeof(System.String));
            dt.Columns.Add(fieldName, typeof(System.String));
            dt.Columns.Add(paramValue, typeof(System.String));
            dt.Columns.Add(enumCondation, typeof(System.Enum));
            dt.Columns.Add(conditionsRelation, typeof(System.Enum));
            dt.Columns.Add(fieldType, typeof(System.Enum));
            dt.PrimaryKey = new DataColumn[1] { dt.Columns[conditionId] };
            dt.TableName = TConditions;
            this.Tables.Add(dt);
            this.DataSetName = "DSTConditions";
        }
        /// <summary>
        /// 
        /// </summary>
        public DBConditions()
        {
            this.BuildData();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageParams"></param>
        public DBConditions(PageParams pageParams)
        {
            if (pageParams != null)
            {
                this.ReturnFields = pageParams.ReturnFields;
                this.PageIndex = pageParams.PageIndex;
                this.PageSize = pageParams.PageSize;
                this.PageSorts = pageParams.PageSorts;
            }
            this.BuildData();
        }
    }
}
