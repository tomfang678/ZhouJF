using System.Collections.Generic;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfRoleSpecialPermissionDAL : IDataAccess<BfRoleSpecialPermissionInfo>
    {
        void BatchAdd(List<BfRoleSpecialPermissionInfo> roleSpecialPermissionInfos);

        int DeleteByRoleId(int roleId);
    }
}
