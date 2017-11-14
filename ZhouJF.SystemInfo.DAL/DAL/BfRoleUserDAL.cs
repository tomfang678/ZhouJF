using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;
using YHPT.SystemInfo.Model.BfRoleUser;

namespace YHPT.SystemInfo.DAL.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_ROLE_USER", "BfRoleUser.xml")]
    public class BfRoleUserDAL : DALBaseSqlServer<BfRoleUserInfo>, IBfRoleUserDAL
    {
        public BfRoleUserDAL()
            : base(typeof(BfRoleUserDAL))
        { }

        public List<BfRoleInfo> GetRoleByUser(int userId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfRoleInfo>("BfRoleUser.xml",
                    "SelectRoleByUser", userId);
                return result;
            }
        }

        public void BatchAdd(List<BfRoleUserInfo> roleInfoList)
        {
            DatabaseExecuteHelper.NewInstance.ExecuteWithTransaction((tran) =>
            {
                if (roleInfoList != null && roleInfoList.Count > 0)
                {
                    int id = GetEntityIdentity("BF_ROLE_USER", roleInfoList.Count);
                    foreach (var tmp in roleInfoList)
                    {
                        tmp.ID = id;
                        id++;
                    }
                }
                using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran))
                {
                    bulk.WriteToServer("BfRoleUser", "AddObject", roleInfoList);
                }
            });
        }

        public int DeleteByUserId(int UserId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfRoleUser", "DeleteByUserId", UserId);
                return i;
            }
        }

        public int DeleteByRoleId(int roleId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfRoleUser", "DeleteByRoleId", roleId);
                return i;
            }
        }
    }
}
