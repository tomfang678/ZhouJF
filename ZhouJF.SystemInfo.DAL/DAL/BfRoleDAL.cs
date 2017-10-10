using MB.Framework.RuleBase.DataAccess;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_ROLE", "BfRole.xml")]
    public class BfRoleDAL : DALBaseSqlServer<BfRoleInfo>, IBfRoleDAL
    {
        public BfRoleDAL()
            : base(typeof(BfRoleDAL))
        { }
    }
}
