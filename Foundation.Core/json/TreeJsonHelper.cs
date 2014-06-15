using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public enum TreeJsonType
    { 
        noLeaf,
        hasLeaf
    }
    public class TreeJsonHelper
    {
        private DataTable _sourceDatatable = null;
        private string _currentIdColumnName = "";
        private string _parentIdColumnName = "";
        private Hashtable _viewColumns = null;
        private string _rootValue = "0";
        private TreeJsonType _jsonType = TreeJsonType.hasLeaf;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceDatatable">数据源</param>
        public TreeJsonHelper(DataTable sourceDatatable, string rootValue)
        {
            this._sourceDatatable = sourceDatatable;
            this._viewColumns = new Hashtable();
            if (!string.IsNullOrEmpty(rootValue))
                _rootValue = rootValue;
        }
        /// <summary>
        /// 设置列关联关系
        /// </summary>
        /// <param name="currentIdColumnName"></param>
        /// <param name="_parentIdColumnName"></param>
        public void SetRelation(string currentIdColumnName
            , string parentIdColumnName)
        {
            this._currentIdColumnName = currentIdColumnName;
            this._parentIdColumnName = parentIdColumnName;
        }

        private string createJson()
        {
            string menulistjson = "{ \"text\":\".\",\"children\": [";

            string treejson = "";

            this.iterator(_rootValue, ref treejson);

            treejson = treejson.Remove(treejson.Length - 1, 1);

            menulistjson += treejson + "]}";
            return menulistjson;

        }

        private void iterator(string parentId, ref String json)
        {
            #region
            DataRow[] drArr = _sourceDatatable.Select(_parentIdColumnName + "=" + parentId); 
            DataRow[] drparent;
            int i = 0;
            foreach (DataRow dr in drArr)
            {
                drparent = _sourceDatatable.Select(_parentIdColumnName + "=" + dr[_currentIdColumnName].ToString());
                json += "{";

                foreach(DictionaryEntry dic in this._viewColumns)
                    json += string.Format("'{0}':'{1}',", dic.Key, dr[dic.Value.ToString()]);

                json += "expanded:true,";

                if (drparent.Length > 0)
                    json += "children:[";
                else if(this._jsonType == TreeJsonType.hasLeaf)
                    json += "leaf:true";

                i++;
                iterator(dr[_currentIdColumnName].ToString(), ref json);
                json += "},";
                if (i == drArr.Length)
                {
                    json = json.Remove(json.Length - 1);
                    json += "]";
                }
            }
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnViewName"></param>
        /// <param name="columnName"></param>
        public void AddColumnView(string columnViewName, string columnName)
        {
            if (!this._viewColumns.ContainsKey(columnViewName))
                this._viewColumns.Add(columnViewName, columnName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnViewName"></param>
        public void AddColumnView(string columnName)
        {
            if (!this._viewColumns.ContainsKey(columnName))
                this._viewColumns.Add(columnName, columnName);
        }

        public string ToString(TreeJsonType type)
        {
            this._jsonType = type;
            return this.createJson();
        }

        public string ToString()
        {
            return this.createJson();
        }
    }
}
