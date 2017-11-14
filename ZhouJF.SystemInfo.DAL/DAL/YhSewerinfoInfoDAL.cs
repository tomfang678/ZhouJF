
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using System.Collections.Generic;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("SewerinfoInfo", "Sewerinfo.xml")]
    public class YhSewerinfoInfoDAL : DALBaseSqlServer<SewerinfoInfo>, IYhSewerinfoInfoDAL
    {
        public YhSewerinfoInfoDAL()
            : base(typeof(YhSewerinfoInfoDAL))
        { }

        private static readonly string OBJECT_XML_FILE = "Sewerinfo";

        public List<SewerinfoInfo> getByRoadId(int roadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<SewerinfoInfo>(OBJECT_XML_FILE,
                    "getByRoadId", roadId);
                return result;
            }
        }
    }
}

