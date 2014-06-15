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
    public class Expression
    {
        public EnumDBReturnAccess EnumReturn;
        public DBConditions SqlConditions = null;
        public String SqlBusiness = "";
        public DataSet WillSaveDs = null;

        public object ReturnValue = null;
        public System.Data.DataSet ReturnDS = new System.Data.DataSet();
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
