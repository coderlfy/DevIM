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
    public class PageSortCollection: System.Collections.CollectionBase
    {
        public string _SortAllString = "";
        /// <summary>
        /// 增加排序
        /// </summary>
        /// <param name="sqlExpression"></param>
        public void Add(PageSort pagesort)
        {
            #region
            for (int i = 0; i < this.Count; i++)
                if (pagesort.Fieldname == ((PageSort)List[i]).Fieldname)
                {
                    ExtConsole.WriteWithColor("不能重复添加排序字段!");
                    return;
                }

            List.Add(pagesort);
            #endregion
        }
        /// <summary>
        /// 删除排序
        /// </summary>
        /// <param name="index"></param>
        public void Remove(int index)
        {
            #region
            if (index > Count - 1 || index < 0)
            {
                ExtConsole.WriteWithColor("添加页排序时，索引出错!");
            }
            else
            {
                List.RemoveAt(index);
            }
            #endregion
        }
        /// <summary>
        /// 设置索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PageSort this[int index]
        {
            #region
            get
            {
                return (PageSort)List[index];
            }
            #endregion
        }

    }
}
