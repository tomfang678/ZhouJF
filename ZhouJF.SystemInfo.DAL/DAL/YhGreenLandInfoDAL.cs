
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("GreenLandInfo", "GreenLandInfo.xml")]
    public class YhGreenLandInfoDAL : DALBaseSqlServer<GreenLandInfo>, IYhGreenLandInfoDAL
    {
        public YhGreenLandInfoDAL()
            : base(typeof(YhGreenLandInfoDAL))
        { }
        private static readonly string OBJECT_XML_FILE = "GreenLandInfo";

        public List<GreenLandInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<GreenLandInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

