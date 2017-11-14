using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRegion;

namespace YHPT.SystemInfo.DAL.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_REGION", "BfRegion.xml")]
    public class BfRegionInfoDAL : DALBaseSqlServer<BfRegionInfo>, IBfRegionInfoDAL
    {
        public BfRegionInfoDAL()
            : base(typeof(BfRegionInfoDAL))
        { }

        public int BatchImport(List<BfRegionInfo> entitys, DataBaseResource dbResource = DataBaseResource.Default)
        {
            if (entitys == null || entitys.Count < 1)
                return 0;
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                var updateMainLst = new List<BfRegionInfo>();
                var insertMainLst = new List<BfRegionInfo>();
                var id = 0;
                #region 组合主表新增及更新

                foreach (var entity in entitys)
                {
                    if (entity.ID > 0)
                        updateMainLst.Add(entity);
                    else
                        insertMainLst.Add(entity);
                }
                if (insertMainLst != null && insertMainLst.Count > 0)
                {
                    id = GetEntityIdentity("BF_CODE", insertMainLst.Count);
                    foreach (var tmp in insertMainLst)
                    {
                        tmp.ID = id;
                        id++;
                    }
                }

                #endregion

                MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteWithTransaction((tran) =>
                {
                    using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran))
                    {
                        bulk.WriteToServer("BfRegion", "AddObject", insertMainLst);
                        DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity(tran, "BfRegion", "UpdateObject", updateMainLst);
                    }
                });
                return entitys.Count;
            }
        }
    }
}
