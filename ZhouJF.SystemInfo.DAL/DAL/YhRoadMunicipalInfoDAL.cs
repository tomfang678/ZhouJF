
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("Roadmunicipalinfo", "Roadmunicipalinfo.xml")]
    public class YhRoadMunicipalInfoDAL : DALBaseSqlServer<RoadMunicipalInfo>, IYhRoadMunicipalInfoDAL
    {
        private static readonly string OBJECT_XML_FILE = "Roadmunicipalinfo";

        public YhRoadMunicipalInfoDAL()
            : base(typeof(YhRoadMunicipalInfoDAL))
        { }
        public List<RoadMunicipalInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<RoadMunicipalInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }

    }
}

