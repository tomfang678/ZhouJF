using System.Collections.Generic;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;
using YHPT.SystemInfo.Model.BfRoleUser;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfRoleUserDAL : IDataAccess<BfRoleUserInfo>
    {
        List<BfRoleInfo> GetRoleByUser(int userId);

        void BatchAdd(List<BfRoleUserInfo> roleInfoList);

        int DeleteByUserId(int UserId);

        int DeleteByRoleId(int roleId);
    }
}