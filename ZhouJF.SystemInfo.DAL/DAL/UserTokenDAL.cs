using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfUser;


namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("UserToken", "UserToken.xml")]
    public class UserTokenDAL : DALBaseSqlServer<UserTokenInfo>, IUserTokenDAL
    {
        public UserTokenDAL()
            : base(typeof(UserTokenDAL))
        { }

        public override int Insert(UserTokenInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity<UserTokenInfo>("UserToken", "AddObject", item);
                return item.USERID;
            }
        }
    }
}
