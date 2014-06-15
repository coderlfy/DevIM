using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessBase
{
    public class GeneralBusinesser
    {
        /*
        public static bool SessionCheckValid(string sessionUser, ref string json)
        {
            #region
            if (string.IsNullOrEmpty(sessionUser))
            {
                JsonHelper jsonhlp = new JsonHelper();
                jsonhlp.AddObjectToJson("msg", "sessioninvalid");
                jsonhlp.AddObjectToJson("success", "false");
                json = jsonhlp.ToString();
                return false;
            }
            else
                return true;
            #endregion
        }
        */
        protected string GetMessageJson(bool isSuccess, string message)
        {
            #region
            JsonHelper jsonhlp = new JsonHelper();
            jsonhlp.AddObjectToJson("success", isSuccess.ToString().ToLower());
            jsonhlp.AddObjectToJson("msg", message);
            return jsonhlp.ToString();
            #endregion
        }

        protected string GetJson(DataSet businessData, int totalCount)
        {
            #region
            JsonHelper jsonhlp = new JsonHelper();
            jsonhlp.GetTopicsJson(businessData.Tables[0]);
            jsonhlp.AddObjectToJson("total", totalCount.ToString());
            jsonhlp.AddObjectToJson("success", "true");
            return jsonhlp.ToString();
            #endregion
        }

        protected delegate void DLSaveFunction();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        protected string UtiSave(DLSaveFunction fun)
        {
            JsonHelper jsonhlp = new JsonHelper();
            try
            {
                fun();
                jsonhlp.AddObjectToJson("success", "true");
            }
            catch (Exception e)
            {
                jsonhlp.AddObjectToJson("success", "false");
                jsonhlp.AddObjectToJson("msg", e.Message);
            }
            return jsonhlp.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="accessor"></param>
        /// <returns></returns>
        protected String Save(DataSet data, GeneralAccessor accessor)
        {
            #region

            return UtiSave(() => {
                accessor.SaveSingleT(data);
            });
            #endregion
        }

        protected string ConvertToBool(object dbvalue)
        {
            #region
            return (dbvalue == System.DBNull.Value) ? "null" : Convert.ToBoolean(dbvalue).ToString().ToLower();
            #endregion
        }
        /*
        protected string escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < ba.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(ba[i + 1].ToString("X2"));

                sb.Append(ba[i].ToString("X2"));
            }
            return sb.ToString();
        }
        */
    }
}
