using System;
using System.Text;
using System.IO;
//using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Fundation.Core
{
    /// <summary>
    /// LogTextOp 的摘要说明。
    /// </summary>
    public class LogTextOp
    {
        /// <summary>
        /// 配置文件域
        /// </summary>
        private const String sectionKey = "CONFIG";
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private const String LogFileName = "\\AppLogfile.log";

        private const String IniFileName = "\\AppConfig.INI";

        private const long m_MaxFileLenth = 1024 * 1024 * 2;
        private String logpath;
        private String inipath;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(
            string lpApplicationName,
            string lpKeyName,
            int nDefault,
            string lpFileName);
        [DllImport("kernel32")]
        private static extern bool GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpString,
            string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(
            string lpAppName,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileSection(
            string lpAppName,
            string lpString,
            string lpFileName);


        public LogTextOp()
        {
            logpath = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
            inipath = AppDomain.CurrentDomain.BaseDirectory + IniFileName;
        }
        public LogTextOp(String path)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            logpath = path;
        }

        public String LogFilePath
        {
            get
            {
                return logpath;
            }
            set
            {
                logpath = value;
            }
        }
        public String IniFilePath
        {
            get
            {
                return inipath;
            }
            set
            {
                inipath = value;
            }
        }
        public int AppendARow(String path, String RowContent)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(RowContent);
            }
            return 0;
        }

        #region 读取日志文件
        /// <summary>
        /// 读取日志文件到数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public String[] ReadLog(String path)
        {
            if (File.Exists(path))
            {

                using (StreamReader rw = new StreamReader(path))
                {

                    return rw.ReadToEnd().Replace("\n\r", "\n").Split('\n');
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 读取日志文件到ListBox
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listbox"></param>
        public void ReadLog(String path, ListBox listbox)
        {
            String[] content = ReadLog(path);
            listbox.Items.Clear();
            if (null == content)
            {
                listbox.Items.Add("空日志或日志文件不存在");
            }
            else
            {
                listbox.Items.AddRange(ReadLog(path));
            }
        }
        /// <summary>
        /// 用记事本打开日志文件
        /// </summary>
        public void ReadLog()
        {
            if (String.Empty == LogFilePath)
            {
                LogFilePath = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
            }

            //	ReadLog(LogFilePath,listbox);

            System.Diagnostics.Process.Start(LogFilePath);
        }
        #endregion

        #region 写日志文件
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="logtime">生成时间</param>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public int WriteLog(String path, String logtime, String LogContent)
        {
            if (File.Exists(path))
            {
                System.IO.FileInfo f = new FileInfo(path);
                if (f.Length > m_MaxFileLenth)
                {
                    f.Delete();
                }
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("[" + logtime + "]");
                sw.WriteLine(LogContent);
                sw.Close();
            }
            return 0;
        }
        /// <summary>
        /// 写日志文件，默认日志生成时间为当前时间
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public int WriteLog(String theLogFileName, String LogContent)
        {
            try
            {
                if (String.Empty == theLogFileName)
                {
                    theLogFileName = "AppLogfile";
                }
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\" + theLogFileName + ".log";
                String logtime = System.DateTime.Now.ToString();
                WriteLog(path, logtime, LogContent);
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 写日志文件，用默认应用程序路径和当前时间
        /// </summary>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public int WriteLog(String LogContent)
        {
            try
            {
                if (String.Empty == LogFilePath)
                {
                    LogFilePath = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
                }
                String logtime = System.DateTime.Now.ToString();
                WriteLog(LogFilePath, logtime, LogContent);
            }
            catch
            {
                return -1;
            }
            return 0;
        }
        #endregion

        #region 以INI方式写日志文件
        /// <summary>
        /// 以INI方式写日志，日志内容作为节标题，节内容为时间
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="logtime">生成时间</param>
        /// <param name="LogContent">日志内容</param>
        /// <returns>-1 发生错误；>=0正常</returns>
        public long WriteIniLog(String path, String logtime, String LogContent)
        {
            try
            {
                if (File.Exists(path))
                {
                    System.IO.FileInfo f = new FileInfo(path);
                    if (f.Length > m_MaxFileLenth)
                    {
                        f.Delete();
                    }
                }
                long i = WritePrivateProfileString(sectionKey, LogContent, logtime, path);

                return i;

            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 写INI日志文件，默认日志生成时间为当前时间
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public long WriteIniLog(String path, String LogContent)
        {
            String logtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return WriteIniLog(path, logtime, LogContent);
        }
        /// <summary>
        /// 写INI日志文件，用默认应用程序路径和当前时间
        /// </summary>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public long WriteIniLog(String LogContent)
        {
            if (String.Empty == LogFilePath)
            {
                LogFilePath = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
            }
            String[] concentarr = LogContent.Split(new char[] { '\r', '\n' });

            String co = "";
            foreach (String content in concentarr)
            {
                co += content;
                co += "-";
            }
            String logtime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return WriteIniLog(LogFilePath, logtime, co);
        }
        /// <summary>
        /// 写INI日志文件，用默认应用程序路径和当前时间
        /// </summary>
        /// <param name="LogContent">日志内容</param>
        /// <returns></returns>
        public long WriteSecIniLog(String title, String LogContent)
        {
            if (String.Empty == LogFilePath)
            {
                LogFilePath = AppDomain.CurrentDomain.BaseDirectory + LogFileName;
            }
            String[] concentarr = LogContent.Split(new char[] { '\r', '\n' });

            String co = "";
            foreach (String content in concentarr)
            {
                co += content;
                co += "-";
            }

            String logtime = co + ",发生时间：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return WriteIniLog(LogFilePath, logtime, title);
        }

        /// <summary>
        /// 写INI文件，用默认应用程序路径
        /// </summary>
        /// <param name="Key">配置变量</param>
        /// <param name="KeyContent">配置变量内容</param>
        /// <returns></returns>
        public long WriteSecIni(String Key, String KeyContent)
        {
            if (String.Empty == inipath)
            {
                inipath = AppDomain.CurrentDomain.BaseDirectory + IniFileName;
            }


            long i = WritePrivateProfileString("CONFIG", Key, KeyContent, inipath);
            return i;

        }
        #endregion

        #region 读写INI文件
        /// <summary>
        /// 使用默认文件名写INI文件
        /// </summary>
        /// <param name="Sectionname">配置节名称</param>
        /// <param name="Keyname">配置变量名</param>
        /// <param name="KeyValue">配置变量值</param>
        /// <returns></returns>
        public long WriteIniFile(String Sectionname, String Keyname, String KeyValue)
        {
            if (String.Empty == inipath)
            {
                inipath = AppDomain.CurrentDomain.BaseDirectory + IniFileName;
            }

            return WritePrivateProfileString(Sectionname, Keyname, KeyValue, inipath);

        }
        public long WriteIniFile(String Sectionname, String Keyname, String KeyValue, String FileName)
        {
            long i = -1;
            String IniFilepath = AppDomain.CurrentDomain.BaseDirectory + "\\" + FileName;
            try
            {
                i = WritePrivateProfileString(Sectionname, Keyname, KeyValue, IniFilepath);
            }
            catch
            {
                i = -1;
            }

            return i;
        }
        public String ReadIniFile(String Sectionname, String Keyname)
        {
            if (String.Empty == inipath)
            {
                inipath = AppDomain.CurrentDomain.BaseDirectory + IniFileName;
            }
            StringBuilder sb = new StringBuilder(255);
            try
            {
                GetPrivateProfileString(Sectionname, Keyname, "", sb, 255, inipath);
            }
            catch
            {
                return String.Empty;
            }
            return sb.ToString();
        }
        public String ReadIniFile(String Sectionname, String Keyname, String FileName)
        {

            inipath = AppDomain.CurrentDomain.BaseDirectory + "\\" + FileName;

            StringBuilder sb = new StringBuilder(255);
            try
            {
                GetPrivateProfileString(Sectionname, Keyname, "", sb, 255, inipath);
            }
            catch
            {
                return String.Empty;
            }
            return sb.ToString();
        }
        #endregion
    }

}
