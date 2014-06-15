/****************************************
***创建人：bhlfy
***创建时间：2014-06-15 11:28:46
***公司：山西博华科技有限公司
***修改人：
***修改时间：
***文件描述：。
*****************************************/
using BusinessBase;
using Fundation.Core;
using System;
using System.Data;
using DevIMDataLibrary;
using DevIMSqlLibrary;

namespace DevIMBusiness
{
    public class FriendshipBusiness : GeneralBusinesser
    {
        private FriendshipClass _friendshipclass = new FriendshipClass();
        #region Create by iCat Assist Tools
        /****************************************
        ***生成器版本：V2.0.0.20540
        ***生成时间：2014-06-15 11:28:46
        ***公司：山西博华科技有限公司
        ***友情提示：以下代码为生成器自动生成，可做参照修改之用；
        ***         如需有其他业务要求，可在region外添加新方法；
        ***         如发现任何编译和运行时错误，请联系QQ：330669393。
        *****************************************/

        #region public members methods
        
        /// <summary>
        /// 根据条件筛选所有Friendship指定页码的数据（分页型）
        /// </summary>
        /// <param name="friendship">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <returns></returns>
        public string GetJsonByPage(EntityFriendship friendship, PageParams pageparams)
        {
            #region
            int totalCount = 0;
            DataSet friendshipdata = this.GetData(friendship, pageparams, out totalCount);
            return base.GetJson(friendshipdata, totalCount);
            #endregion
        }
        
        /// <summary>
        /// 保存friendshipdata数据集数据
        /// </summary>
        /// <param name="friendshipdata">数据集对象</param>
        /// <returns>返回保存后的响应信息</returns>
        public String SaveFriendship(FriendshipData friendshipdata)
        {
            #region
            return base.Save(friendshipdata, this._friendshipclass);
            #endregion
        }
                
        /// <summary>
        /// 添加Friendship表行数据（如主键为非自增型字段，则自行修改代码）
        /// </summary>
        /// <param name="friendshipdata">数据集对象</param>
        /// <param name="friendship">实体对象</param>
        public void AddRow(ref FriendshipData friendshipdata, EntityFriendship friendship)
        {
            #region
            DataRow dr = friendshipdata.Tables[0].NewRow();
            friendshipdata.Assign(dr, FriendshipData.fid, friendship.fid);
            friendshipdata.Assign(dr, FriendshipData.meId, friendship.meId);
            friendshipdata.Assign(dr, FriendshipData.friendId, friendship.friendId);
            friendshipdata.Assign(dr, FriendshipData.writeTime, friendship.writeTime);
            friendshipdata.Tables[0].Rows.Add(dr);
            #endregion
        }
        
        /// <summary>
        /// 编辑friendshipdata数据集中指定的行数据
        /// </summary>
        /// <param name="friendshipdata">数据集对象</param>
        /// <param name="friendship">实体对象</param>
        public void EditRow(ref FriendshipData friendshipdata, EntityFriendship friendship)
        {
            #region
            if (friendshipdata.Tables[0].Rows.Count <= 0)
                friendshipdata = this.getData(friendship.fid);
            DataRow dr = friendshipdata.Tables[0].Rows.Find(new object[1] {friendship.fid});
            friendshipdata.Assign(dr, FriendshipData.fid, friendship.fid);
            friendshipdata.Assign(dr, FriendshipData.meId, friendship.meId);
            friendshipdata.Assign(dr, FriendshipData.friendId, friendship.friendId);
            friendshipdata.Assign(dr, FriendshipData.writeTime, friendship.writeTime);
            #endregion
        }
        		
        /// <summary>
        /// 删除friendshipdata数据集中指定的行数据
        /// </summary>
        /// <param name="friendshipdata">数据集对象</param>
        /// <param name="fid">主键-朋友关系序号</param>
        public void DeleteRow(ref FriendshipData friendshipdata,string fid)
        {
            #region
            if (friendshipdata.Tables[0].Rows.Count <= 0)
                friendshipdata = this.getData(fid);
            DataRow dr = friendshipdata.Tables[0].Rows.Find(new object[1] { fid });
            if (dr != null)
                dr.Delete();
            #endregion
        }
        
        /// <summary>
        /// 获取Friendship数据表的全部数据
        /// </summary>
        /// <returns>Json字符串</returns>
        public string GetJsonByAll()
        {
            #region
            int totalCount = 0;
            FriendshipData friendshipdata = this.getData(null);
            totalCount = friendshipdata.Tables[0].Rows.Count;
            return base.GetJson(friendshipdata, totalCount);
            #endregion
        }
        #endregion

        #region private members methods
        
        /// <summary>
        /// 根据主键值检索符合该条件的记录，用于编辑和删除记录时。
        /// </summary>
        /// <param name="fid">主键-朋友关系序号</param>
        /// <returns></returns>
        private FriendshipData getData(string fid)
        {
            #region
            FriendshipData friendshipdata = new FriendshipData();
            DBConditions querybusinessparams = new DBConditions();
            querybusinessparams.Add(FriendshipData.fid, EnumSqlType.sqlint, EnumCondition.Equal, fid);
            this._friendshipclass.GetSingleTAllWithoutCount(friendshipdata, querybusinessparams);   
            return friendshipdata;
            #endregion
        }
        
        /// <summary>
        /// 根据条件筛选所有Friendship指定页码的数据（分页型）
        /// </summary>
        /// <param name="friendship">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <param name="totalCount">符合条件的记录总数量</param>
        /// <returns></returns>
        public FriendshipData GetData(EntityFriendship friendship, PageParams pageparams, out int totalCount)
        {
            #region
            DBConditions querybusinessparams = new DBConditions(pageparams);
            querybusinessparams.Add(FriendshipData.fid, EnumSqlType.sqlint, 
                EnumCondition.Equal, friendship.fid);
            querybusinessparams.Add(FriendshipData.meId, EnumSqlType.sqlint, 
                EnumCondition.Equal, friendship.meId);
            querybusinessparams.Add(FriendshipData.friendId, EnumSqlType.sqlint, 
                EnumCondition.Equal, friendship.friendId);
            querybusinessparams.Add(FriendshipData.writeTime, EnumSqlType.datetime, 
                EnumCondition.Equal, friendship.writeTime);
            FriendshipData friendshipdata = new FriendshipData();
            totalCount = this._friendshipclass.GetSingleT(friendshipdata, querybusinessparams);
            return friendshipdata;
            #endregion
        }
        #endregion

        #endregion
    }
}


