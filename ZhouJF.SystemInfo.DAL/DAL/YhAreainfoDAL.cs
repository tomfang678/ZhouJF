
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhAreaInfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("AreaInfo", "Areainfo.xml")]
    public class YhAreainfoDAL : DALBaseSqlServer<AreaInfo>, IYhAreainfoDAL
    {
        public YhAreainfoDAL()
            : base(typeof(YhAreainfoDAL))
        { }



        public System.Collections.Generic.List<string> GetAllRegion()
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<string>("Areainfo.xml",
                    "GetAllRegion");
                return result;
            }
        }
    }
}

