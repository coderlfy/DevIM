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
using System.Security.Cryptography;
using System.Text;

namespace Fundation.Core
{
    public class Password
    {
        public static string ToMD5(string orginal)
        {
            #region
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bt = Encoding.Default.GetBytes(orginal);
            byte[] result = md5.ComputeHash(bt);
            return BitConverter.ToString(result).Replace("-", ""); //
            #endregion
        }

    }
}
