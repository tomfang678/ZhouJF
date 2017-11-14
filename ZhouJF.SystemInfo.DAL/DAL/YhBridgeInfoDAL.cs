
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BridgeinfoInfo", "Bridgeinfo.xml")]
    public class YhBridgeInfoDAL : DALBaseSqlServer<BridgeInfo>, IYhBridgeInfoDAL
    {
        public YhBridgeInfoDAL()
            : base(typeof(YhBridgeInfoDAL))
        { }
        private static readonly string OBJECT_XML_FILE = "Bridgeinfo";

        public List<BridgeInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BridgeInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

