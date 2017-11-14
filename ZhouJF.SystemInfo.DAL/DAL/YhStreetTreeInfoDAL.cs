
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("StreetTreeInfo", "StreetTreeInfo.xml")]
    public class YhStreetTreeInfoDAL : DALBaseSqlServer<StreetTreeInfo>, IYhStreetTreeInfoDAL
    {
        public YhStreetTreeInfoDAL()
            : base(typeof(YhStreetTreeInfoDAL))
        { }
        private static readonly string OBJECT_XML_FILE = "StreetTreeInfo";

        public List<StreetTreeInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<StreetTreeInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

