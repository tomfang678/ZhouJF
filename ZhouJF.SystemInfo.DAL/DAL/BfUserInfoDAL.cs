using MB.Framework.RuleBase.DataAccess;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfUser;
using MB.Framework.Common.Enums;
using MB.RuleBase.Common;
using MB.Orm.DB;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_USER", "BfUser.xml")]
    public class BfUserInfoDAL : DALBaseSqlServer<BfUserInfo>, IBfUserDAL
    {
        public BfUserInfoDAL()
            : base(typeof(BfUserInfoDAL))
        { }

        public bool ChangePwd(BfUserInfo bfUser)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity("BfUser", "ChangePWD", bfUser);
                return i>0;
            }
        }
    }
}
