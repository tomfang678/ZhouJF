using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_ROLE_MOD_SPECIAL_PERMISSION", "BfRoleSpecialPermission.xml")]
    public class BfRoleSpecialPermissionDAL : DALBaseSqlServer<BfRoleSpecialPermissionInfo>, IBfRoleSpecialPermissionDAL
    {
        public BfRoleSpecialPermissionDAL()
            : base(typeof(BfRoleSpecialPermissionDAL))
        { }

        public void BatchAdd(List<BfRoleSpecialPermissionInfo> roleSpecialPermissionInfos)
        {
            DatabaseExecuteHelper.NewInstance.ExecuteWithTransaction((tran) =>
            {
                if (roleSpecialPermissionInfos != null && roleSpecialPermissionInfos.Count > 0)
                {
                    int id = GetEntityIdentity("BF_ROLE_MOD_SPECIAL_PERMISSION", roleSpecialPermissionInfos.Count);
                    foreach (var tmp in roleSpecialPermissionInfos)
                    {
                        tmp.ID = id;
                        id++;
                    }
                }
                using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran))
                {
                    bulk.WriteToServer("BfRoleSpecialPermission", "AddObject", roleSpecialPermissionInfos);
                }
            });
        }

        public int DeleteByRoleId(int roleId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfRoleSpecialPermission", "DeleteByRoleId", roleId);
                return i;
            }
        }
    }
}
