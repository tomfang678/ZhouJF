
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("StreetTreeDtlInfo", "StreetTreeDtlInfo.xml")]
    public class YhStreetTreeDtlDAL : DALBaseSqlServer<StreetTreeDtlInfo>, IYhStreetTreeDtlDAL
    {
        public YhStreetTreeDtlDAL()
            : base(typeof(YhStreetTreeDtlDAL))
        { }
        private static readonly string OBJECT_XML_FILE = "StreetTreeDtlInfo";

        public List<StreetTreeDtlInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<StreetTreeDtlInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

