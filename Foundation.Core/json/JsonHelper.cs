/****************************************
###创建人：lify
###创建时间：不记得了
###公司：Cat Studio
###最后修改时间：2012-04-03
###最后修改人：lify
###修改摘要：
****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Web.Script.Serialization;
using System.Data;

namespace Fundation.Core
{
    public class JsonHelper
    {
        private string _jsonString = "";
        /// <summary>
        /// 构造数据表记录的Json串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public JsonHelper GetTopicsJson(DataTable dt)
        {
            #region
            #region V1.0如此会存在bug：只能被webapp来调用
            //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //jsSerializer.RegisterConverters(new JavaScriptConverter[]{new DataTableConverter()});
            #endregion

            this.addModule(DataTableConverter.Serialize(dt));
            return this;
            #endregion
        }

        public JsonHelper GetTopicsJson(DataTable dt, string jsonTableName)
        {
            #region
            this.addModule(DataTableConverter.Serialize(dt, jsonTableName));
            return this;
            #endregion
        }
        /// <summary>
        /// 将属性写入Json串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonHelper AddObjectToJson(string key, string value)
        {
            #region
            string json = "";
            if (value.IndexOf("[") != -1 && value.IndexOf("]") != -1)
                json = "{\"" + key + "\":" + JsonFilter.Exec(value) + "}";
            else
                json = "{\"" + key + "\":\"" + JsonFilter.Exec(value) + "\"}";

            this.addModule(json);
            return this;
            #endregion
        }
        /// <summary>
        /// 将属性写入Json串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonHelper AddObjectToJson(string key, bool value)
        {
            #region
            string json = "{\"" + key + "\":\"" + value.ToString().ToLower() + "\"}";
            this.addModule(json);
            return this;
            #endregion
        }
        public JsonHelper SetResult(bool value)
        {
            #region
            this.AddObjectToJson("success", value);
            return this;
            #endregion
        }
        /// <summary>
        /// 输出Json串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this._jsonString;
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public void WriteError(Exception e)
        {
            this._jsonString = "";
            this.AddObjectToJson("success", "false");
            this.AddObjectToJson("msg", JsonFilter.Exec(e.Message));
        }
        /// <summary>
        /// 拼接Json串
        /// </summary>
        /// <param name="jsonModule"></param>
        private void addModule(string jsonModule)
        {
            #region
            if (this._jsonString != String.Empty)
                this._jsonString = this._jsonString.Insert(1, jsonModule.Substring(1, jsonModule.Length - 2)+",");
            else
                this._jsonString = jsonModule;
            #endregion
        }
        
    }
}
