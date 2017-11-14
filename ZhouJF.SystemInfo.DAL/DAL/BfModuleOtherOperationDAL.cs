using System.Collections.Generic;
using System.Text;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_MODULE_OTHER_OPERATION", "BfModuleOtherOperation.xml")]
    public class BfModuleOtherOperationDAL : DALBaseSqlServer<BfModuleOtherOperationInfo>, IBfModuleOtherOperationDAL
    {
        public BfModuleOtherOperationDAL()
            : base(typeof(BfModuleOtherOperationDAL))
        { }

        public int DeleteByModuleID(int moduleID, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfModuleOtherOperation.xml", "DeleteObjectByModuleID", moduleID);
                return i;
            }
        }

        public List<BfModuleOtherOperationInfo> SelectOtherOperationByModule(List<int> moduleIds)
        {
            var result = new List<BfModuleOtherOperationInfo>();
            var index = 0;
            var sb = new StringBuilder();
            foreach (var id in moduleIds)
            {
                index++;
                sb.Append(id + ",");
                if (index > 950)
                {
                    result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleOtherOperationInfo>("BfModuleOtherOperation", "SelectOtherOperationByModule", sb.ToString().TrimEnd(',')));
                    index = 0;
                    sb = new StringBuilder();
                }
            }
            if (index > 0 && sb.ToString().Length > 0)
            {
                result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleOtherOperationInfo>("BfModuleOtherOperation", "SelectOtherOperationByModule", sb.ToString().TrimEnd(',')));
            }
            return result;
        }
    }
}
