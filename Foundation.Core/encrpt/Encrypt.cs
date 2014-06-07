using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fundation.Core
{
    public class Encrypt
    {
        private static SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();
        private static string CIV = "kXwL7X2+fgM=";// 初始化向量
        private static string CKEY = "FwGQWRRgKCI=";//密钥 

        //public Encrypt()
        //{
        //    mCSP = new DESCryptoServiceProvider();
        //}

        public static string EncryptString(string Value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            ct = mCSP.CreateEncryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV));

            byt = Encoding.UTF8.GetBytes(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
        /*
        public static string DecryptString(string Value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            ct = mCSP.CreateDecryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV));

            byt = Convert.FromBase64String(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }
        */
    }
}
