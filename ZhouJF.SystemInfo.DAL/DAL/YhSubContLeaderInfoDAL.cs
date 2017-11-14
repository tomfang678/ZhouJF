
using MB.Framework.RuleBase.DataAccess;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhManager;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("SubContLeaderInfo", "Subcontleaderinfo.xml")]
    public class YhSubContLeaderInfoDAL : DALBaseSqlServer<SubContLeaderInfo>, IYhSubContLeaderInfoDAL
    {
        public YhSubContLeaderInfoDAL()
            : base(typeof(YhSubContLeaderInfoDAL))
        { }

    }
}

