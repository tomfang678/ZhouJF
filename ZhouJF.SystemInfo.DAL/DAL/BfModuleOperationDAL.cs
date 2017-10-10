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
    [MB.Framework.RuleBase.ObjectDataMapping("BF_MODULE_OPERATION", "BfModuleOperation.xml")]
    public class BfModuleOperationDAL : DALBaseSqlServer<BfModuleOperationInfo>, IBfModuleOperationDAL
    {
        private static readonly string OBJECT_XML_FILE = "BfModuleOperation";
        public BfModuleOperationDAL()
            : base(typeof(BfModuleOperationDAL))
        { }

        public int DeleteByModuleID(int moduleID, DataBaseResource dbResource = DataBaseResource.Default)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(dbResource)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfModuleOperation.xml", "DeleteObjectByModuleID", moduleID);
                return i;
            }
        }

        public List<BfModuleOperationInfo> SelectOperationByUserID(int UserID)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleOperationInfo>(OBJECT_XML_FILE,
                    "SelectOperationByUserID", UserID);
                return result;
            }
        }

        public List<BfModuleOperationInfo> SelectOperationByModule(List<int> moduleIds)
        {
            var result = new List<BfModuleOperationInfo>();
            var index = 0;
            var sb = new StringBuilder();
            foreach (var id in moduleIds)
            {
                index++;
                sb.Append(id + ",");
                if (index > 950)
                {
                    result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleOperationInfo>(OBJECT_XML_FILE, "SelectOperationByModule", sb.ToString().TrimEnd(',')));
                    index = 0;
                    sb = new StringBuilder();
                }
            }
            if (index > 0 && sb.ToString().Length > 0)
            {
                result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfModuleOperationInfo>(OBJECT_XML_FILE, "SelectOperationByModule", sb.ToString().TrimEnd(',')));
            }
            return result;
        }
    }
}
