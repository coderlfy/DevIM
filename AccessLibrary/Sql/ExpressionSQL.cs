﻿/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 03:00:29
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using Fundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessLibrary
{
    class ExpressionSQL : Expression
    {
        public override string ToString()
        {
            return base.SqlBusiness;
        }
    }
}
