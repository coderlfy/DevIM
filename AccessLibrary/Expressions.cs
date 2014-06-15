using Fundation.Core;
/****************************************
***创建人：bhlfy
***创建时间：2013-08-29 01:47:25
***公司：iCat Studio
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
    public class Expressions : System.Collections.CollectionBase, ICloneable
    {
        public int GeneralCommandCount = 0;
        public int AdvanceCommandCount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumReturn"></param>
        private void addCount(EnumDBReturnAccess enumReturn)
        {
            switch (enumReturn)
            {
                case EnumDBReturnAccess.ExeNoQuery:
                case EnumDBReturnAccess.Scalar:
                    this.GeneralCommandCount++;
                    break;
                case EnumDBReturnAccess.FillDsByCustom:
                case EnumDBReturnAccess.FillDsByStoredProcedure:
                case EnumDBReturnAccess.SaveDS:
                case EnumDBReturnAccess.FillDsWithoutPage:
                    this.AdvanceCommandCount++;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumReturn"></param>
        private void subtractCount(EnumDBReturnAccess enumReturn)
        {
            switch (enumReturn)
            {
                case EnumDBReturnAccess.ExeNoQuery:
                case EnumDBReturnAccess.Scalar:
                    this.GeneralCommandCount--;
                    break;
                case EnumDBReturnAccess.FillDsByCustom:
                case EnumDBReturnAccess.FillDsByStoredProcedure:
                case EnumDBReturnAccess.SaveDS:
                case EnumDBReturnAccess.FillDsWithoutPage:
                    this.AdvanceCommandCount--;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlExpression"></param>
        public void Add(Expression sqlExpression)
        {
            this.addCount(sqlExpression.EnumReturn);
            List.Add(sqlExpression);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                ExtConsole.WriteWithColor("往事务中添加Action时，索引出错!");
            }
            else
            {
                this.subtractCount(((Expression)List[index]).EnumReturn);
                List.RemoveAt(index);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Expression this[int index]
        {
            get
            {
                return (Expression)List[index];
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        protected override void OnClear()
        {
            this.GeneralCommandCount = 0;
            this.AdvanceCommandCount = 0;
            base.OnClear();

        }
    }
}
