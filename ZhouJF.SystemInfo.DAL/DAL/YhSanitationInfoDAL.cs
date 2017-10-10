
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("SanitationInfo", "Sanitationinfo.xml")]
    public class YhSanitationInfoDAL : DALBaseSqlServer<SanitationInfo>, IYhSanitationInfoDAL
    {
        public YhSanitationInfoDAL()
            : base(typeof(YhSanitationInfoDAL))
        { }

        private static readonly string OBJECT_XML_FILE = "Sanitationinfo";
        public List<SanitationInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<SanitationInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

