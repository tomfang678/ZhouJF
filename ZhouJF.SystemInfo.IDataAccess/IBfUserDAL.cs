using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfUserDAL : IDataAccess<BfUserInfo>
    {
        bool ChangePwd(BfUserInfo bfUser);
    }
}
