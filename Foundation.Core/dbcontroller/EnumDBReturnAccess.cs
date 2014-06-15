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
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public enum EnumDBReturnAccess
    {
        /// <summary>
        /// 返回结果集中第一行第一列值
        /// </summary>
        Scalar,
        /// <summary>
        /// 返回影响记录条数
        /// </summary>
        ExeNoQuery,
        /// <summary>
        /// 通过存储过程填充数据集（检索）
        /// </summary>
        FillDsByStoredProcedure,
        /// <summary>
        /// 通过普通SQL语句填充数据集（检索）
        /// </summary>
        FillDsByCustom,
        /// <summary>
        /// 填充数据集不需分页（一般用于统计报表）
        /// </summary>
        FillDsWithoutPage,
        /// <summary>
        /// 保存数据集
        /// </summary>
        SaveDS
    }
}
