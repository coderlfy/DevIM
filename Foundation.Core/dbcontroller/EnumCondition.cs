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
    public enum EnumCondition
    {
        #region
        /// <summary>
        /// 相等
        /// </summary>
        Equal,
        /// <summary>
        /// 不等
        /// </summary>
        NotEqual,
        /// <summary>
        /// in类型查询
        /// </summary>
        InValues,
        /// <summary>
        /// 双边相似型
        /// </summary>
        LikeBoth,
        /// <summary>
        /// 左%相似
        /// </summary>
        LikeLeft,
        /// <summary>
        /// 右%相似
        /// </summary>
        LikeRight,
        /// <summary>
        /// 大于
        /// </summary>
        Greater,
        /// <summary>
        /// 小于
        /// </summary>
        Less,
        /// <summary>
        /// 大于或等于
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// 小于或等于
        /// </summary>
        LessOrEqual,
        /// <summary>
        /// 空为NULL
        /// </summary>
        EmptyIsNull,
        /// <summary>
        /// 不为空
        /// </summary>
        IsNotNull
        #endregion
    }

    public enum EnumConditionsRelation
    {
        /// <summary>
        /// 和（并且）
        /// </summary>
        And,
        /// <summary>
        /// 或者
        /// </summary>
        Or
    }
}
