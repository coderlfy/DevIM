/****************************************
***创建人：bhlfy
***创建时间：2014-06-15 11:28:46
***公司：山西iCat Studio有限公司
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
    public class TUserBusiness : GeneralBusinesser
    {
        private TUserClass _tuserclass = new TUserClass();
        #region Create by iCat Assist Tools
        /****************************************
        ***生成器版本：V2.0.0.20540
        ***生成时间：2014-06-15 11:28:46
        ***公司：山西iCat Studio有限公司
        ***友情提示：以下代码为生成器自动生成，可做参照修改之用；
        ***         如需有其他业务要求，可在region外添加新方法；
        ***         如发现任何编译和运行时错误，请联系QQ：330669393。
        *****************************************/

        #region public members methods
        
        /// <summary>
        /// 根据条件筛选所有TUser指定页码的数据（分页型）
        /// </summary>
        /// <param name="tuser">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <returns></returns>
        public string GetJsonByPage(EntityTUser tuser, PageParams pageparams)
        {
            #region
            int totalCount = 0;
            DataSet tuserdata = this.GetData(tuser, pageparams, out totalCount);
            return base.GetJson(tuserdata, totalCount);
            #endregion
        }
        
        /// <summary>
        /// 保存tuserdata数据集数据
        /// </summary>
        /// <param name="tuserdata">数据集对象</param>
        /// <returns>返回保存后的响应信息</returns>
        public String SaveTUser(TUserData tuserdata)
        {
            #region
            return base.Save(tuserdata, this._tuserclass);
            #endregion
        }
                
        /// <summary>
        /// 添加TUser表行数据（如主键为非自增型字段，则自行修改代码）
        /// </summary>
        /// <param name="tuserdata">数据集对象</param>
        /// <param name="tuser">实体对象</param>
        public void AddRow(ref TUserData tuserdata, EntityTUser tuser)
        {
            #region
            DataRow dr = tuserdata.Tables[0].NewRow();
            tuserdata.Assign(dr, TUserData.uid, tuser.uid);
            tuserdata.Assign(dr, TUserData.userid, tuser.userid);
            tuserdata.Assign(dr, TUserData.userpwd, tuser.userpwd);
            tuserdata.Assign(dr, TUserData.userfullName, tuser.userfullName);
            tuserdata.Assign(dr, TUserData.writeTime, tuser.writeTime);
            tuserdata.Tables[0].Rows.Add(dr);
            #endregion
        }
        
        /// <summary>
        /// 编辑tuserdata数据集中指定的行数据
        /// </summary>
        /// <param name="tuserdata">数据集对象</param>
        /// <param name="tuser">实体对象</param>
        public void EditRow(ref TUserData tuserdata, EntityTUser tuser)
        {
            #region
            if (tuserdata.Tables[0].Rows.Count <= 0)
                tuserdata = this.getData(tuser.uid);
            DataRow dr = tuserdata.Tables[0].Rows.Find(new object[1] {tuser.uid});
            tuserdata.Assign(dr, TUserData.uid, tuser.uid);
            tuserdata.Assign(dr, TUserData.userid, tuser.userid);
            tuserdata.Assign(dr, TUserData.userpwd, tuser.userpwd);
            tuserdata.Assign(dr, TUserData.userfullName, tuser.userfullName);
            tuserdata.Assign(dr, TUserData.writeTime, tuser.writeTime);
            #endregion
        }
        		
        /// <summary>
        /// 删除tuserdata数据集中指定的行数据
        /// </summary>
        /// <param name="tuserdata">数据集对象</param>
        /// <param name="uid">主键-用户序列号</param>
        public void DeleteRow(ref TUserData tuserdata,string uid)
        {
            #region
            if (tuserdata.Tables[0].Rows.Count <= 0)
                tuserdata = this.getData(uid);
            DataRow dr = tuserdata.Tables[0].Rows.Find(new object[1] { uid });
            if (dr != null)
                dr.Delete();
            #endregion
        }
        
        /// <summary>
        /// 获取TUser数据表的全部数据
        /// </summary>
        /// <returns>Json字符串</returns>
        public string GetJsonByAll()
        {
            #region
            int totalCount = 0;
            TUserData tuserdata = this.getData(null);
            totalCount = tuserdata.Tables[0].Rows.Count;
            return base.GetJson(tuserdata, totalCount);
            #endregion
        }
        #endregion

        #region private members methods
        
        /// <summary>
        /// 根据主键值检索符合该条件的记录，用于编辑和删除记录时。
        /// </summary>
        /// <param name="uid">主键-用户序列号</param>
        /// <returns></returns>
        private TUserData getData(string uid)
        {
            #region
            TUserData tuserdata = new TUserData();
            DBConditions querybusinessparams = new DBConditions();
            querybusinessparams.Add(TUserData.uid, EnumSqlType.sqlint, EnumCondition.Equal, uid);
            this._tuserclass.GetSingleTAllWithoutCount(tuserdata, querybusinessparams);   
            return tuserdata;
            #endregion
        }
        
        /// <summary>
        /// 根据条件筛选所有TUser指定页码的数据（分页型）
        /// </summary>
        /// <param name="tuser">实体对象</param>
        /// <param name="pageparams">分页对象</param>
        /// <param name="totalCount">符合条件的记录总数量</param>
        /// <returns></returns>
        public TUserData GetData(EntityTUser tuser, PageParams pageparams, out int totalCount)
        {
            #region
            DBConditions querybusinessparams = new DBConditions(pageparams);
            querybusinessparams.Add(TUserData.uid, EnumSqlType.sqlint, 
                EnumCondition.Equal, tuser.uid);
            querybusinessparams.Add(TUserData.userid, EnumSqlType.nvarchar, 
                EnumCondition.Equal, tuser.userid);
            querybusinessparams.Add(TUserData.userpwd, EnumSqlType.nvarchar, 
                EnumCondition.Equal, tuser.userpwd);
            querybusinessparams.Add(TUserData.userfullName, EnumSqlType.nvarchar, 
                EnumCondition.Equal, tuser.userfullName);
            querybusinessparams.Add(TUserData.writeTime, EnumSqlType.datetime, 
                EnumCondition.Equal, tuser.writeTime);
            TUserData tuserdata = new TUserData();
            totalCount = this._tuserclass.GetSingleT(tuserdata, querybusinessparams);
            return tuserdata;
            #endregion
        }
        #endregion

        #endregion
    }
}


