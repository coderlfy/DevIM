#region Create by iCat Assist Tools
/****************************************
***生成器版本：V2.0.0.20540
***创建人：bhlfy
***生成时间：2014-06-15 14:47:40
***公司：iCat Studio
***友情提示：本文件为生成器自动生成，切勿手动更改
***         如发现任何编译和运行时的错误，请联系QQ：330669393。
*****************************************/
using System;
using System.Data;
using Fundation.Core;
            
namespace DevIMDataLibrary
{
    public class GroupData : DataLibBase
    {
        
        /// <summary>
        /// 组序号。
        /// </summary>
        public const string gid = "gid";
        /// <summary>
        /// 用户序列号。
        /// </summary>
        public const string uid = "uid";
        /// <summary>
        /// 组名。
        /// </summary>
        public const string groupName = "groupName";
        /// <summary>
        /// 录入时间。
        /// </summary>
        public const string writeTime = "writeTime";
        /// <summary>
        /// 表名。
        /// </summary>
        public const string Group = "Group";
        
        private void BuildData()
        {
            DataTable dt = new DataTable(Group);
            
            dt.Columns.Add(gid, typeof(System.Int32));
            dt.Columns.Add(uid, typeof(System.Int32));
            dt.Columns.Add(groupName, typeof(System.String));
            dt.Columns.Add(writeTime, typeof(System.DateTime));
            dt.PrimaryKey = new DataColumn[1] { dt.Columns[gid] };
            dt.TableName = Group;
            this.Tables.Add(dt);
            this.DataSetName = "TGroup";
        }

        public GroupData()
        {
            this.BuildData();
        }
    }
}
#endregion

