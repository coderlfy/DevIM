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
    public class FriendshipData : DataLibBase
    {
        
        /// <summary>
        /// 朋友关系序号。
        /// </summary>
        public const string fid = "fid";
        /// <summary>
        /// 用户序列号。
        /// </summary>
        public const string meId = "meId";
        /// <summary>
        /// 朋友序号。
        /// </summary>
        public const string friendId = "friendId";
        /// <summary>
        /// 组序号。
        /// </summary>
        public const string gid = "gid";
        /// <summary>
        /// 维护时间。
        /// </summary>
        public const string writeTime = "writeTime";
        /// <summary>
        /// 表名。
        /// </summary>
        public const string Friendship = "Friendship";
        
        private void BuildData()
        {
            DataTable dt = new DataTable(Friendship);
            
            dt.Columns.Add(fid, typeof(System.Int32));
            dt.Columns.Add(meId, typeof(System.Int32));
            dt.Columns.Add(friendId, typeof(System.Int32));
            dt.Columns.Add(gid, typeof(System.Int32));
            dt.Columns.Add(writeTime, typeof(System.DateTime));
            dt.PrimaryKey = new DataColumn[1] { dt.Columns[fid] };
            dt.TableName = Friendship;
            this.Tables.Add(dt);
            this.DataSetName = "TFriendship";
        }

        public FriendshipData()
        {
            this.BuildData();
        }
    }
}
#endregion

