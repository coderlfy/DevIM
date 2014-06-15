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
    public class DataLibBase: DataSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentRow"></param>
        /// <param name="colName"></param>
        /// <param name="colValue"></param>
        public void Assign(DataRow currentRow, string colName, object colValue)
        {
            if (colValue != null)
                currentRow[colName] = SqlType.ConvertForSQL(colValue, Type.GetTypeCode(this.Tables[0].Columns[colName].DataType));
        }

    }
}
