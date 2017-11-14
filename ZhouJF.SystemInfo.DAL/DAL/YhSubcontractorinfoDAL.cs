
using MB.Framework.RuleBase.DataAccess;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.Subcontractor;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("SubContractorInfo", "SubContractorInfo.xml")]
    public class YhSubcontractorinfoDAL : DALBaseSqlServer<SubContractorInfo>, IYhSubcontractorinfoDAL
    {
        public YhSubcontractorinfoDAL()
            : base(typeof(YhSubcontractorinfoDAL))
        { }

    }
}

