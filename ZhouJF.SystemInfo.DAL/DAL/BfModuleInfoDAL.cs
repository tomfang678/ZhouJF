using System.Collections.Generic;
using System.Transactions;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_MODULE", "BfModule.xml")]
    public class BfModuleInfoDAL : DALBaseSqlServer<BfModuleInfo>, IBfModuleDAL
    {
        private static readonly string OBJECT_XML_FILE = "BfModule";
        public BfModuleInfoDAL()
            : base(typeof(BfModuleInfoDAL))
        { }

        public override int Insert(BfModuleInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                int mainId = 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    mainId = base.Insert(item, dbResource);
                    if (item.OperationItems != null)
                    {
                        item.OperationItems.ForEach(n =>
                        {
                            n.BF_MODULE_ID = mainId;
                            (new BfModuleOperationDAL()).Insert(n, dbResource);
                        });
                    }
                    if (item.OtherOperationItems != null)
                    {
                        item.OtherOperationItems.ForEach(n =>
                        {
                            n.BF_MODULE_ID = mainId;
                            (new BfModuleOtherOperationDAL()).Insert(n, dbResource);
                        });
                    }
                    scope.Complete();
                }
                return mainId;
            }
        }

        public override bool Update(BfModuleInfo item, DataBaseResource dbResource = DataBaseResource.Default)
        {
            bool updateResult = false;
            using (TransactionScope scope = new TransactionScope())
            {
                updateResult = base.Update(item, dbResource);
                var oldOperateList = (new BfModuleOperationDAL()).GetItems(new List<QueryParameterInfo>()
                {
                    new QueryParameterInfo("BF_MODULE_ID", item.ID, DataFilterConditions.Equal)
                });
                if (item.OperationItems != null)
                {
                    if (oldOperateList != null)
                    {
                        foreach (var operate in oldOperateList)
                        {
                            if (!item.OperationItems.Exists(p => p.CODE == operate.CODE))
                            {
                                //没有该操作，将删除
                                (new BfModuleOperationDAL()).Delete(operate.ID);
                            }
                        }
                        foreach (var operate in item.OperationItems)
                        {
                            if (!oldOperateList.Exists(p => p.CODE == operate.CODE))
                            {
                                //没有该操作，将新增
                                (new BfModuleOperationDAL()).Insert(operate, dbResource);
                            }
                        }
                    }
                    else
                    {
                        item.OperationItems.ForEach(o =>
                        {
                            o.BF_MODULE_ID = item.ID;
                            if (o.ID == 0)
                                (new BfModuleOperationDAL()).Insert(o, dbResource);
                        });

                    }
                }
                else
                {
                    foreach (var operate in oldOperateList)
                    {
                        (new BfModuleOperationDAL()).Delete(operate.ID);
                    }
                    
                }
                if (item.OtherOperationItems != null)
                {
                    item.OtherOperationItems.ForEach(o =>
                    {
                        o.BF_MODULE_ID = item.ID;
                        if (o.ID == 0)
                            (new BfModuleOtherOperationDAL()).Insert(o, dbResource);
                        else
                            (new BfModuleOtherOperationDAL()).Update(o, dbResource);
                    });
                }
                scope.Complete();
            }
            return updateResult;
        }

        public List<BfModuleInfo> SelectObjectByUserID(int UserID)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleInfo>(OBJECT_XML_FILE,
                    "SelectObjectByUserID", UserID);
                return result;
            }
        }

        public List<BfModuleInfo> SelectAllChildBfModuleInfos()
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleInfo>(OBJECT_XML_FILE,
                    "SelectAllChildBfModuleInfos");
                return result;
            }
        }
    }
}
