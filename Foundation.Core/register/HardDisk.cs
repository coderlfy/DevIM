/****************************************
***创建人：bhlfy
***创建时间：2013-11-12 09:00:29
***公司：ICat科技
***文件描述：磁盘。
*****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace Fundation.Core
{
    public class HardDisk
    {
        public static string GetSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            //网上有提到，用Win32_DiskDrive，但是用Win32_DiskDrive获得的硬盘信息中并不包含SerialNumber属性。  
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;

            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["Model"].Value.ToString();
                break;
            }
            return strID;
        }
    }
}
