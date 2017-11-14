
using MB.Framework.RuleBase.DataAccess;
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

    }
}

