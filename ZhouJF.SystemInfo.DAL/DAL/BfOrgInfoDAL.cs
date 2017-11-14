using System.Transactions;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfOrg;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_ORG", "BfOrg.xml")]
    public class BfOrgDAL : DALBaseSqlServer<BfOrgInfo>, IBfOrgDAL
    {
        public BfOrgDAL()
            : base(typeof(BfOrgDAL))
        { }

        public override int Insert(BfOrgInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                int mainId = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    mainId = base.Insert(item, dbResource);
                    //if (item.OWNER_ID < 1)
                    //{
                    //    item.OWNER_ID = mainId;
                    //    base.Update(item, dbResource);
                    //}
                    scope.Complete();
                }
                return mainId;
            }
        }
    }
}
