/****************************************
***创建人：bhlfy
***创建时间：2014-06-15 14:47:44
***公司：iCat Studio
***修改人：
***修改时间：
***文件描述：。
*****************************************/

using BusinessBase;
using DevIMDataLibrary;
using Fundation.Core;
using System;
using System.Data;

namespace DevIMSqlLibrary
{
    public class TUserClass : GeneralAccessor
    {
        #region 自定义业务关联访问
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public DataSet SelectUserByGroup(DBConditions conditions)
        {
            #region
            DataSet userbygroupdata = new DataSet();
            string businessSql = @"SELECT a.[uid],d.[uid] as friendId, a.[userid], d.[userid] as friendQQ,
	c.[groupName], a.[userfullName], d.[UserFullName] as friendFullname from [Friendship] b
	inner join [TUser] a  on a.[uid] = b.[meId] 
	inner join [Group] c on b.[gid] = c.[gid] 
	left join [TUser] d on b.[friendid] = d.[uid] ";

            conditions.AddKeys(TUserData.uid);

            base.GetWithoutPageBusiness(
                businessSql, userbygroupdata, conditions);

            return userbygroupdata;
            #endregion
        }

        #endregion
    }
}


