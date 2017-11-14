
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("Roadbasicinfo", "Roadbasicinfo.xml")]
    public class YhRoadbasicinfoDAL : DALBaseSqlServer<Roadbasicinfo>, IYhRoadbasicinfoDAL
    {
        private static readonly string OBJECT_XML_FILE = "Roadbasicinfo";

        public YhRoadbasicinfoDAL()
            : base(typeof(YhRoadbasicinfoDAL))
        { }
    }
}

