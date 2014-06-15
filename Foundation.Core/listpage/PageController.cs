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
    public class PageController
    {
        public PageParams _pageParams = new PageParams();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderfld"></param>
        /// <param name="orderByType"></param>
        public void AddSorts(string orderfld, EnumSQLOrderBY orderByType)
        {
            #region
            if (_pageParams.PageSorts == null)
                _pageParams.PageSorts = new PageSortCollection();

            PageSort pagesort = new PageSort(orderfld, orderByType);
            _pageParams.PageSorts.Add(pagesort);
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public void AddViewFields(string fieldName)
        {
            #region
            if (_pageParams.ReturnFields == null)
                _pageParams.ReturnFields = fieldName;
            else
                _pageParams.ReturnFields += string.Format(",{0}", fieldName);
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_mPageIndex">当前页码</param>
        /// <param name="_mPageSize">每页记录数</param>
        public void SetPageParamsByPageIndex(int _mPageIndex, int _mPageSize)
        {
            #region
            _pageParams.PageIndex = _mPageIndex;
            _pageParams.PageSize = _mPageSize;
            #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_start"></param>
        /// <param name="_mLimit"></param>
        public void SetPageParamsByPageStart(int _start, int _mLimit)
        {
            #region
            int pageindex = 0;
            if (_mLimit != 0)
                pageindex = (_mLimit + _start) / _mLimit;

            _pageParams.PageIndex = pageindex;
            _pageParams.PageSize = _mLimit;
            #endregion
        }

    }
}
