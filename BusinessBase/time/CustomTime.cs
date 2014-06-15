using AccessLibrary;
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessBase
{
    public class CustomTime
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private static DateTime getDBTime(string connectString)
        {
            #region
            IDBAccess dbaccess = new DBAccess();
            DateTime time;
            try
            {
                string sql = "select getdate()";

                if (!string.IsNullOrEmpty(connectString))
                    dbaccess.SetConnectString(connectString);

                dbaccess.AddAction(sql, EnumDBReturnAccess.Scalar);
                dbaccess.StartActions();

                time = Convert.ToDateTime(dbaccess.Actions[0].ReturnValue);
                dbaccess.ClearActions();

                return time;
            }
            finally
            {
                dbaccess.Dispose();
            }

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDBServerTime()
        {
            #region
            return getDBTime("");

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static DateTime GetDBServerTime(string connectString)
        {
            #region
            return getDBTime(connectString);

            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentServerTime()
        {
            #region
            return DateTime.Now;
            #endregion
        }
    }
}
