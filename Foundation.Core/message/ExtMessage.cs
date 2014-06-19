/****************************************
###创建人：lify
###创建时间：2012-02-26
###公司：ICat科技
###最后修改时间：2012-02-26
###最后修改人：lify
###修改摘要：
****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fundation.Core
{
    public class ExtMessage
    {
        /// <summary>
        /// 不允许为空的提示框
        /// </summary>
        /// <param name="lb"></param>
        /// <returns></returns>
        public static DialogResult NotAllowEmpty(Label lb)
        {
            #region
            string viewAttr = FormatString(lb.Text.Trim());
            return MessageBox.Show(viewAttr + "不为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion
        }
        /// <summary>
        /// 应为数值的提示框
        /// </summary>
        /// <param name="lb"></param>
        /// <returns></returns>
        public static DialogResult IsNotNum(Label lb)
        {
            #region
            string viewAttr = FormatString(lb.Text.Trim());
            return MessageBox.Show(viewAttr + "应为数值类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion
        }
        /// <summary>
        /// 系统提示框
        /// </summary>
        /// <param name="information">提示内容</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string information)
        {
            #region
            return MessageBox.Show(information, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion
        }
        public static DialogResult ShowError(string information)
        {
            #region
            return MessageBox.Show(information, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            #endregion
        }
        /// <summary>
        /// 格式化字符串-替换字符
        /// </summary>
        /// <param name="oldString">原始字符串</param>
        /// <returns>格式化后的字符串</returns>
        private static string FormatString(string oldString)
        {
            #region
            string newString = oldString;
            newString = newString.Replace("：", "");
            newString = newString.Replace(":", "");
            newString = newString.Replace(" ", "");
            return newString;
            #endregion
        }
        /// <summary>
        /// 是否确认框
        /// </summary>
        /// <param name="information">提示内容</param>
        /// <returns>对话框结果</returns>
        public static DialogResult ShowConfirm(string information)
        {
            #region
            return MessageBox.Show(information, "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            #endregion
        }
    }
}
