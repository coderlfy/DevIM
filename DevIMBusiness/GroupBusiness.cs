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
    public class GroupBusiness : GeneralBusinesser
    {
        private GroupClass _groupclass = new GroupClass();
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
        /// 根据条件筛选所有Group指定页码的数据（分页型）
        /// </summary>
        /// <param name="group">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <returns></returns>
        public string GetJsonByPage(EntityGroup group, PageParams pageparams)
        {
            #region
            int totalCount = 0;
            DataSet groupdata = this.GetData(group, pageparams, out totalCount);
            return base.GetJson(groupdata, totalCount);
            #endregion
        }
        
        /// <summary>
        /// 保存groupdata数据集数据
        /// </summary>
        /// <param name="groupdata">数据集对象</param>
        /// <returns>返回保存后的响应信息</returns>
        public String SaveGroup(GroupData groupdata)
        {
            #region
            return base.Save(groupdata, this._groupclass);
            #endregion
        }
                
        /// <summary>
        /// 添加Group表行数据（如主键为非自增型字段，则自行修改代码）
        /// </summary>
        /// <param name="groupdata">数据集对象</param>
        /// <param name="group">实体对象</param>
        public void AddRow(ref GroupData groupdata, EntityGroup group)
        {
            #region
            DataRow dr = groupdata.Tables[0].NewRow();
            groupdata.Assign(dr, GroupData.gid, group.gid);
            groupdata.Assign(dr, GroupData.uid, group.uid);
            groupdata.Assign(dr, GroupData.groupName, group.groupName);
            groupdata.Assign(dr, GroupData.writeTime, group.writeTime);
            groupdata.Tables[0].Rows.Add(dr);
            #endregion
        }
        
        /// <summary>
        /// 编辑groupdata数据集中指定的行数据
        /// </summary>
        /// <param name="groupdata">数据集对象</param>
        /// <param name="group">实体对象</param>
        public void EditRow(ref GroupData groupdata, EntityGroup group)
        {
            #region
            if (groupdata.Tables[0].Rows.Count <= 0)
                groupdata = this.getData(group.gid);
            DataRow dr = groupdata.Tables[0].Rows.Find(new object[1] {group.gid});
            groupdata.Assign(dr, GroupData.gid, group.gid);
            groupdata.Assign(dr, GroupData.uid, group.uid);
            groupdata.Assign(dr, GroupData.groupName, group.groupName);
            groupdata.Assign(dr, GroupData.writeTime, group.writeTime);
            #endregion
        }
        		
        /// <summary>
        /// 删除groupdata数据集中指定的行数据
        /// </summary>
        /// <param name="groupdata">数据集对象</param>
        /// <param name="gid">主键-组序号</param>
        public void DeleteRow(ref GroupData groupdata,string gid)
        {
            #region
            if (groupdata.Tables[0].Rows.Count <= 0)
                groupdata = this.getData(gid);
            DataRow dr = groupdata.Tables[0].Rows.Find(new object[1] { gid });
            if (dr != null)
                dr.Delete();
            #endregion
        }
        
        /// <summary>
        /// 获取Group数据表的全部数据
        /// </summary>
        /// <returns>Json字符串</returns>
        public string GetJsonByAll()
        {
            #region
            int totalCount = 0;
            GroupData groupdata = this.getData(null);
            totalCount = groupdata.Tables[0].Rows.Count;
            return base.GetJson(groupdata, totalCount);
            #endregion
        }
        #endregion

        #region private members methods
        
        /// <summary>
        /// 根据主键值检索符合该条件的记录，用于编辑和删除记录时。
        /// </summary>
        /// <param name="gid">主键-组序号</param>
        /// <returns></returns>
        private GroupData getData(string gid)
        {
            #region
            GroupData groupdata = new GroupData();
            DBConditions querybusinessparams = new DBConditions();
            querybusinessparams.Add(GroupData.gid, EnumSqlType.sqlint, EnumCondition.Equal, gid);
            this._groupclass.GetSingleTAllWithoutCount(groupdata, querybusinessparams);   
            return groupdata;
            #endregion
        }
        
        /// <summary>
        /// 根据条件筛选所有Group指定页码的数据（分页型）
        /// </summary>
        /// <param name="group">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <param name="totalCount">符合条件的记录总数量</param>
        /// <returns></returns>
        public GroupData GetData(EntityGroup group, PageParams pageparams, out int totalCount)
        {
            #region
            DBConditions querybusinessparams = new DBConditions(pageparams);
            querybusinessparams.Add(GroupData.gid, EnumSqlType.sqlint, 
                EnumCondition.Equal, group.gid);
            querybusinessparams.Add(GroupData.uid, EnumSqlType.sqlint, 
                EnumCondition.Equal, group.uid);
            querybusinessparams.Add(GroupData.groupName, EnumSqlType.nvarchar, 
                EnumCondition.Equal, group.groupName);
            querybusinessparams.Add(GroupData.writeTime, EnumSqlType.datetime, 
                EnumCondition.Equal, group.writeTime);
            GroupData groupdata = new GroupData();
            totalCount = this._groupclass.GetSingleT(groupdata, querybusinessparams);
            return groupdata;
            #endregion
        }
        #endregion

        #endregion
    }
}


