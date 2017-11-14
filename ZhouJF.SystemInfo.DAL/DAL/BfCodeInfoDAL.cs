using System.Collections.Generic;
using System.Transactions;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_CODE", "BfCode.xml")]
    public class BfCodeInfoDAL : DALBaseSqlServer<BfCodeInfo>, IBfCodeInfoDAL
    {
        public BfCodeInfoDAL()
            : base(typeof(BfCodeInfoDAL))
        { }

        public override int Insert(BfCodeInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                int mainId = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    mainId = base.Insert(item, dbResource);
                    if (item.BfCodeDtlInfoList != null)
                    {
                        item.BfCodeDtlInfoList.ForEach(n =>
                        {
                            n.BF_CODE_ID = mainId;
                            (new BfCodeDetailInfoDAL()).Insert(n, dbResource);
                        });
                    }
                    scope.Complete();
                }
                return mainId;
            }
        }

        public override bool Update(BfCodeInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            bool updateResult = false;
            using (TransactionScope scope = new TransactionScope())
            {
                updateResult = base.Update(item, dbResource);
                if (item.BfCodeDtlInfoList != null)
                {
                    item.BfCodeDtlInfoList.ForEach(o =>
                    {
                        o.BF_CODE_ID = item.ID;
                        if (o.ID == 0)
                            (new BfCodeDetailInfoDAL()).Insert(o, dbResource);
                        else
                            (new BfCodeDetailInfoDAL()).Update(o, dbResource);
                    });
                }
                scope.Complete();
            }
            return updateResult;
        }

        public int BatchImport(List<BfCodeInfo> entitys, DataBaseResource dbResource = DataBaseResource.Default)
        {
            if (entitys == null || entitys.Count < 1)
                return 0;
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                var updateMainLst = new List<BfCodeInfo>();
                var insertMainLst = new List<BfCodeInfo>();
                var dtlLst = new List<BfCodeDetailInfo>();
                var updateDtlLst = new List<BfCodeDetailInfo>();
                var insertDtlLst = new List<BfCodeDetailInfo>();
                var id = 0;
                #region 组合主表新增及更新

                foreach (var entity in entitys)
                {
                    if(entity.ID > 0)
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
                        tmp.BfCodeDtlInfoList.ForEach(n =>
                        {
                            n.BF_CODE_ID = id;
                        });
                        id++;
                    }
                }

                #endregion

                #region 组合细表新增及更新

                entitys.ForEach(n =>
                {
                    dtlLst.AddRange(n.BfCodeDtlInfoList);
                });

                foreach (var entity in dtlLst)
                {
                    if (entity.ID > 0)
                        updateDtlLst.Add(entity);
                    else
                        insertDtlLst.Add(entity);
                }
                id = GetEntityIdentity("BF_CODE_DETAIL", insertDtlLst.Count);
                insertDtlLst.ForEach(n =>
                {
                    n.ID = id;
                    id++;
                });

                #endregion

                MB.RuleBase.Common.DatabaseExecuteHelper.NewInstance.ExecuteWithTransaction((tran) =>
                {
                    using (var bulk = MB.RuleBase.BulkCopy.DbBulkExecuteFactory.CreateDbBulkExecute(tran))
                    {
                        bulk.WriteToServer("BfCode", "AddObject", insertMainLst);
                        DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity(tran, "BfCode", "UpdateObject", updateMainLst);
                        bulk.WriteToServer("BfCodeDetail", "AddObject", insertDtlLst);
                        DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQueryByEntity(tran, "BfCodeDetail", "UpdateObject", updateDtlLst);
                    }
                });
                return entitys.Count;
            }
        }
    }
}
