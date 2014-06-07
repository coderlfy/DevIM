using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fundation.Core
{
    public class OS
    {
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        public static extern bool LogonUser(string userName, string domainName,
            string password, int LogonType, int LogonProvider, ref IntPtr phToken);

        public static bool IsValidateCredentials(
            string userName, string password)
        {
            #region
            string domain = "MyDomain";

            IntPtr tokenHandler = IntPtr.Zero;

            bool isValid = LogonUser(userName, 
                domain, password, 2, 0, ref tokenHandler);

            return isValid;
            #endregion
        }
    }
}
