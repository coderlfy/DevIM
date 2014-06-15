/****************************************
###创建人：lify
###创建时间：不记得了
###公司：Cat Studio
###最后修改时间：2012-04-03
###最后修改人：lify
###修改摘要：
****************************************/
#region V1.0
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;

namespace JsonHlpLib
{
    public class DataTableConverter : JavaScriptConverter
    {
        /// <summary>
        /// 重写序列化方式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            #region
            DataTable dt = obj as DataTable;
            Dictionary<string, object> result = new Dictionary<string, object>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    row.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                rows.Add(row);
            }
            result["topics"] = rows;
            return result;
            #endregion
        }



        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            #region
            throw new NotImplementedException();
            #endregion
        }
        /// <summary>
        /// 设置支持的序列化对象类型
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            #region
            get
            {
                return new Type[] { typeof(DataTable) };
            }
            #endregion
        }
    }
}
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fundation.Core
{
    public class DataTableConverter
    {
        /// <summary>
        /// 重写序列化方式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        private static string CreateSerialize(DataTable dt, string jsonTablename)
        {
            #region
            string result = "";
            string rowjson = "";
            string topicname = (jsonTablename == null) ? "topics" : jsonTablename;
            foreach (DataRow dr in dt.Rows)
            {
                string coljson = "";
                string tmpValue = "";
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.DataType == Type.GetType("System.Boolean"))
                    {
                        tmpValue = (dr[dc.ColumnName] == System.DBNull.Value) ? "null" : Convert.ToBoolean(dr[dc.ColumnName]).ToString().ToLower();
                        coljson += String.Format("\"{0}\":{1},", dc.ColumnName, tmpValue);
                    }
                    else if (dc.DataType == Type.GetType("System.DateTime"))
                    {
                        tmpValue = (dr[dc.ColumnName] == System.DBNull.Value) ? "" : Convert.ToDateTime(dr[dc.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                        coljson += String.Format("\"{0}\":\"{1}\",", dc.ColumnName, tmpValue);
                    }
                    else
                    {
                        coljson += String.Format("\"{0}\":\"{1}\",", dc.ColumnName, JsonFilter.Exec(dr[dc.ColumnName].ToString()));
                    }
                }
                coljson = coljson.Remove(coljson.Length - 1, 1);
                rowjson += "{" + coljson + "},";
            }
            if (dt.Rows.Count > 0)
                rowjson = rowjson.Remove(rowjson.Length - 1, 1);
            result = "{\"" + topicname + "\":[" + rowjson + "]}";
            return result;
            #endregion
        }

        public static string Serialize(DataTable dt)
        {
            return CreateSerialize(dt, null);
        }

        public static string Serialize(DataTable dt, string jsonTablename)
        {
            return CreateSerialize(dt, jsonTablename);
        }
        /*
        public object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            #region
            throw new NotImplementedException();
            #endregion
        }
         
        /// <summary>
        /// 设置支持的序列化对象类型
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            #region
            get
            {
                return new Type[] { typeof(DataTable) };
            }
            #endregion
        }
        */
    }
}

