using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_ROLE_MOD_PERMISSION", "BfRolePermission.xml")]
    public class BfRolePermissionDAL : DALBaseSqlServer<BfRolePermissionInfo>, IBfRolePermissionDAL
    {
        public BfRolePermissionDAL()
            : base(typeof(BfRolePermissionDAL))
        { }

        public void BatchAdd(List<BfRolePermissionInfo> rolePermissionInfos)
        {
            DatabaseExecuteHelper.NewInstance.ExecuteWithTransaction((tran) =>
            {
                if (rolePermissionInfos != null && rolePermissionInfos.Count > 0)
                {
                    int id = GetEntityIdentity("BF_ROLE_MOD_PERMISSION", rolePermissionInfos.Count);
                    foreach (var tmp in rolePermissionInfos)
                    {
                        tmp.ID = id;
                        id++;
                    }
                }
                using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran))
                {
                    bulk.WriteToServer("BfRolePermission", "AddObject", rolePermissionInfos);
                }
            });
        }

        public int DeleteByRoleId(int roleId)
        {

            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfRolePermission", "DeleteByRoleId", roleId);
                return i;
            }
        }
    }
}
