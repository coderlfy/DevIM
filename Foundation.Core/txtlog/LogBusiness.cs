using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Collections;
using System.Net;

namespace Fundation.Core
{
    public class LogBusiness
    {
        private string dirName = "";
        private string logFileName = "";
        private int maxLogFileCount = 5;
        /// <summary>
        /// 获取日志目录下信息
        /// </summary>
        /// <param name="dirName">目录名</param>
        public LogBusiness(string dirName)
        {
            this.dirName = dirName;
            string dirPath = getDirPath();
            //目录检测
            this.checkDir(dirPath);
        }
        /// <summary>
        /// 写操作日志时可用
        /// </summary>
        /// <param name="dirName">目录名</param>
        /// <param name="logFileName">日志文件名</param>
        public LogBusiness(string dirName, string logFileName)
        {
            this.dirName = dirName;
            this.logFileName = logFileName;
            string dirPath = getDirPath();
            this.checkDir(dirPath);
        }
        /// <summary>
        /// 目录检测-如不存在则创建
        /// </summary>
        /// <param name="dirPath"></param>
        private void checkDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }
        /// <summary>
        /// 获取目录全路径
        /// </summary>
        /// <returns></returns>
        private string getDirPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + this.dirName;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        public void writefile(string content)
        {
            string logFilePath = getDirPath()+"\\"+this.logFileName;

            StreamWriter sw;
            try
            {
                if (!File.Exists(logFilePath))
                    sw = File.CreateText(logFilePath);
                else
                    sw = File.AppendText(logFilePath);
                sw.WriteLine(content);
                sw.WriteLine();
                sw.Dispose();
            }
            catch (Exception e)
            {
                writefile(e.ToString());
            }
        }

        public string getDir()
        {
            return getDirPath() + "\\" + this.logFileName;
        }
        
        public DataTable getFileList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("LogRecordID", typeof(int)));
            dt.Columns.Add(new DataColumn("LogRecordName", typeof(string)));
            DataRow dr;
            
            FileData[] fData = FastDirectoryEnumerator.GetFiles(this.getDirPath(), "*", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < fData.Length ; i++)
            {
                dr = dt.NewRow();
                dr["LogRecordID"] = i;
                dr["LogRecordName"] = fData[i].Name;
                dt.Rows.InsertAt(dr, i);
            }
            return dt;
        }

        public void clearLog()
        {
            FileData[] fData = FastDirectoryEnumerator.GetFiles(this.getDirPath(), "*", SearchOption.TopDirectoryOnly);
            string logContent = "";
            if (fData.Length > maxLogFileCount)//换参
            {
                ArrayList fileArr = new ArrayList();
                foreach (FileData file in fData)
                    fileArr.Add(file.Name);
                fileArr.Sort();

                for (int i = 0; i < fileArr.Count - maxLogFileCount; i++)//换参
                {
                    File.Delete(this.getDirPath()+ "\\" + fileArr[i].ToString());
                    logContent = String.Format("events:clear log file {0}\r\ndatetime:{1}", fileArr[i].ToString(), DateTime.Now.ToString());
                    this.writefile(logContent);
                }
            }
        }
    }
}
