/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
***公司：山西博华科技有限公司
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    class ExpressionOracle : Expression
    {
        public override string ToString()
        {
            return base.SqlBusiness;
        }
    }
}
