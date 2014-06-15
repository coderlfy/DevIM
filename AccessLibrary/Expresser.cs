using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    class Expresser
    {
        #region 私有变量和常量字符串
        private string _business = "";
        private string _condition = "";
        private string _viewTableName = "";
        private const string conditionInitFormat = " ({0} {1} '{2}') {3} ";
        private const string conditionLeftLikeFormat = " ({0} {1} '%{2}') {3} ";
        private const string conditionRightLikeFormat = " ({0} {1} '{2}%') {3} ";
        private const string conditionBothLikeFormat = " ({0} {1} '%{2}%') {3} ";
        private const string conditionEmptyIsNullFormat = " ({0} is null) {1} ";
        private const string conditionIsNotNullFormat = " ({0} is not null) {1} ";
        private const string conditionInValuesFormat = " ({0} in ({1})) {2} ";
        private string _orderbyfield = "";
        #endregion

        /// <summary>
        /// 排序字段
        /// </summary>
        public String OrderByField
        {
            #region
            get
            {
                return _orderbyfield;
            }
            set
            {
                _orderbyfield = value;
            }
            #endregion
        }
        /// <summary>
        /// 业务对象名称（也即虚拟表名）
        /// </summary>
        public String ViewTableName
        {
            #region
            get
            {
                return _viewTableName;
            }
            set
            {
                _viewTableName = value;
            }
            #endregion
        }
        /// <summary>
        /// 业务关联语句
        /// </summary>
        public String Business
        {
            #region
            get
            {
                return _business;
            }
            set
            {
                _business = value;
            }
            #endregion
        }
        /// <summary>
        /// 检索条件
        /// </summary>
        public String Condition
        {
            #region
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
            #endregion
        }
        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="allConditions"></param>
        /// <returns></returns>
        private void addCondition(DBConditions allConditions)
        {
            #region
            _condition = "";

            DataRowCollection drcollect = allConditions.Tables[0].Rows;
            for (int i = 0; i < drcollect.Count; i++)
            {
                DataRow dr = drcollect[i];
                EnumCondition conditiontype = (EnumCondition)dr[DBConditions.enumCondation];
                EnumSqlType fieldtype = (EnumSqlType)dr[DBConditions.fieldType];
                EnumConditionsRelation conditionsrelation = (EnumConditionsRelation)dr[DBConditions.conditionsRelation];
                getCondition(conditiontype, dr[DBConditions.fieldName], 
                    dr[DBConditions.paramValue], fieldtype, conditionsrelation);
            }
            if (_condition != string.Empty)
                _condition = _condition.Remove(_condition.Length - 4, 4);
            //可在最后一个条件时，不予补And。
            #endregion
        }
        /// <summary>
        /// 根据传入的各字段参数及类型动态构造SQL语句
        /// </summary>
        /// <param name="conditionType">条件类型</param>
        /// <param name="fieldName">SQL字段名</param>
        /// <param name="paramValue">查询值</param>
        private void getCondition(EnumCondition conditionType,
            object fieldName, object paramValue, EnumSqlType fieldtype,
            EnumConditionsRelation conditionsRelation)
        {
            #region
            string convertsqlvalue = "";
            string relation = (conditionsRelation == EnumConditionsRelation.And) ? "and" : "or";
            switch (conditionType)
            {
                case EnumCondition.IsNotNull:
                    if (paramValue.ToString() == string.Empty)
                        _condition += String.Format(conditionIsNotNullFormat, 
                            fieldName, relation);
                    break;
                case EnumCondition.EmptyIsNull:
                    if (paramValue.ToString() == string.Empty)
                        _condition += String.Format(conditionEmptyIsNullFormat, 
                            fieldName, relation);
                    else
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, "=", paramValue, relation);
                    break;
                case EnumCondition.Equal:
                    convertsqlvalue = paramValue.ToString();
                    if (convertsqlvalue != string.Empty)
                    {
                        if (fieldtype == EnumSqlType.bit)
                            convertsqlvalue = Convert.ToBoolean(paramValue) ? "1" : "0";
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, "=", convertsqlvalue, relation);
                    }
                    break;
                case EnumCondition.LikeBoth:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionBothLikeFormat, 
                            fieldName, "like", paramValue, relation);
                    break;
                case EnumCondition.LikeLeft:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionLeftLikeFormat, 
                            fieldName, "like", paramValue, relation);
                    break;
                case EnumCondition.LikeRight:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionRightLikeFormat, 
                            fieldName, "like", paramValue, relation);
                    break;
                case EnumCondition.Greater:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, ">", paramValue, relation);
                    break;
                case EnumCondition.GreaterOrEqual:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, ">=", paramValue, relation);
                    break;
                case EnumCondition.LessOrEqual:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, "<=", paramValue, relation);
                    break;
                case EnumCondition.Less:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionInitFormat, 
                            fieldName, "<", paramValue, relation);
                    break;
                case EnumCondition.InValues:
                    if (paramValue.ToString() != string.Empty)
                        _condition += String.Format(conditionInValuesFormat, 
                            fieldName, paramValue, relation);
                    break;
                default:
                    break;
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public String GetViewTablename(Expression expression)
        {
            #region
            return (expression.ReturnDS.Tables.Count <= 0) ? 
                "tablename" : expression.ReturnDS.Tables[0].TableName;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderbyType"></param>
        /// <returns></returns>
        private static String getAscOrDesc(EnumSQLOrderBY orderbyType)
        {
            #region
            return (orderbyType == EnumSQLOrderBY.ASC) ? "ASC" : "DESC";
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        private static String getOrderby(Expression expression)
        {
            #region
            string orderby = "";
            string format = "";
            if (expression.SqlConditions != null)
            {
                DBConditions conditions = expression.SqlConditions;

                if (conditions.PageSorts != null
                    && expression.SqlBusiness.IndexOf("count(*)") < 0)
                {
                    for (int i = 0; i < conditions.PageSorts.Count; i++)
                    {
                        PageSort pagesort = conditions.PageSorts[i];
                        format = (i == 0) ? "{0} {1}" : ",{0} {1}";
                        orderby += string.Format(format,
                            pagesort.Fieldname, getAscOrDesc(pagesort.OrderByType));
                    }
                    if (conditions.PageSorts.Count > 0)
                        orderby = string.Format("order by {0}", orderby);
                }
            }
            return orderby;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private static string getKeysByReturnDs(DataSet ds)
        {
            #region
            string keys = "";
            if (ds.Tables.Count > 0)
            {
                DataColumn[] dc = ds.Tables[0].PrimaryKey;
                for (int i = 0; i < dc.Length; i++)
                {
                    string temp = dc[i].ColumnName;

                    if (i == 0)
                        keys += temp;
                    else
                        keys += string.Format("+{0}", temp);
                }
            }
            return keys;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static string getAllKeys(Expression expression)
        {
            #region
            string keys = "";
            if (expression.SqlConditions != null)
            {
                List<string> dskeys = expression.SqlConditions.DataSetKeys;
                if (dskeys != null)
                {
                    for (int i = 0; i < dskeys.Count; i++)
                    {
                        string temp = dskeys[i];

                        if (i == 0)
                            keys += temp;
                        else
                            keys += string.Format("+{0}", temp);
                    }
                }
                else
                    keys = getKeysByReturnDs(expression.ReturnDS);
            }
            else
                keys = getKeysByReturnDs(expression.ReturnDS);

            if (keys == "") ExtConsole.Write("系统提示：未指定关联键，sql语句构建出错！");
            return keys;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public String CreateTopSelectSQL(Expression expression)
        {
            #region
            if (expression.SqlConditions != null)
                addCondition(expression.SqlConditions);

            int pagesize = int.MaxValue; 
            int pageindex = 1;
            string returnfields = "*";
            string businesssql = expression.SqlBusiness;

            if (expression.SqlConditions != null)
            {
                pagesize = expression.SqlConditions.PageSize;
                pageindex = expression.SqlConditions.PageIndex;
                if(!string.IsNullOrEmpty(expression.SqlConditions.ReturnFields))
                    returnfields = expression.SqlConditions.ReturnFields;
            }

            string key = getAllKeys(expression); 
            string format = @"select top {0} {2} from ({4}) as a
where {3} not in (select top ({0}*({1}-1)) {3} from ({4}) as b {5}) {5}
";
            object[] argument = new object[6];
            argument[0] = pagesize;
            argument[1] = pageindex;
            argument[2] = returnfields;
            argument[3] = key;
            argument[4] = string.IsNullOrEmpty(_condition) ? 
                businesssql : string.Format("{0} where {1}", businesssql, _condition);
            argument[5] = getOrderby(expression);

            format = string.Format(format, argument);
            ExtConsole.WriteWithColor(format);
            return format;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public String CreateSelectSQLWithoutPage(Expression expression)
        {
            #region
            if (expression.SqlConditions != null)
                addCondition(expression.SqlConditions);

            string returnfields = "*";
            if (expression.SqlConditions != null)
            {
                if (!string.IsNullOrEmpty(expression.SqlConditions.ReturnFields))
                    returnfields = expression.SqlConditions.ReturnFields;
            }

            string format = @"select {0} from ({1}) as a {2} {3}";
            object[] argument = new object[4];
            argument[0] = returnfields;
            argument[1] = expression.SqlBusiness;
            argument[2] = string.IsNullOrEmpty(_condition) ?
                "" : string.Format("where {0}", _condition);
            argument[3] = getOrderby(expression);

            format = string.Format(format, argument);
            ExtConsole.WriteWithColor(format);
            return format;
            #endregion
        }

        /// <summary>
        /// 构造通用最终用的sql语句
        /// </summary>
        /// <returns></returns>
        public String CreateGeneralSelectSQL(Expression expression)
        {
            #region
            if (expression.SqlConditions != null)
                addCondition(expression.SqlConditions);

            string endsql = expression.SqlBusiness;

            if (!string.IsNullOrEmpty(_condition))
                endsql = String.Format("{0} where {1} {2}",
                    endsql, _condition, getOrderby(expression));
            else
                endsql = String.Format("{0} {1}",
                    endsql, getOrderby(expression));
            ExtConsole.WriteWithColor(endsql);
            return endsql;
            #endregion
        }

        /// <summary>
        /// 构造存储过程最终用的sql语句
        /// </summary>
        /// <returns></returns>
        public String CreateSelectSQL(Expression expression)
        {
            #region
            if (expression.SqlConditions != null)
                addCondition(expression.SqlConditions);

            string endsql = "";

            if (!string.IsNullOrEmpty(_condition))
                _condition = string.Format("where {0}", _condition);

            endsql = String.Format("({0} {2}) as {1}",
                expression.SqlBusiness, GetViewTablename(expression), _condition);

            ExtConsole.WriteWithColor(endsql);
            return endsql;
            #endregion
        }
        /// <summary>
        /// 构造最终的检索条数的SQL语句
        /// </summary>
        /// <returns></returns>
        public String CreateCountSQL()
        {
            #region
            string endsql = String.Format("select count(*) from {0}", 
                _business);
            if (_condition != string.Empty)
                endsql = String.Format("{0} where {1}", 
                    endsql, _condition);
            return endsql;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="willSaveDs"></param>
        /// <returns></returns>
        public String CreateBuilderSelect(DataSet willSaveDs)
        {
            #region
            DataTable willsavedt = willSaveDs.Tables[0];
            string tablename = willsavedt.TableName;

            if (string.IsNullOrEmpty(tablename))
                ExtConsole.Write("欲保存的数据集表名为NULL或者为空值！");
            if (willsavedt.PrimaryKey.Length <= 0)
                ExtConsole.Write("欲保存的数据集没有主键，不能自动生成sql语句！");

            string keyfieldname = willSaveDs.Tables[0].PrimaryKey[0].ColumnName;
            string sqlexpression = String.Format("select * from [{0}] where {1}='' ",
                tablename, keyfieldname);
            return sqlexpression;
            #endregion
        }

    }
}
