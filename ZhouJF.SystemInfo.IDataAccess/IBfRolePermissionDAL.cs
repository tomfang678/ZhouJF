using System.Collections.Generic;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfRolePermissionDAL : IDataAccess<BfRolePermissionInfo>
    {
        void BatchAdd(List<BfRolePermissionInfo> rolePermissionInfos);

        int DeleteByRoleId(int roleId);
    }
}
