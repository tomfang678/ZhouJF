using System.Collections.Generic;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfCodeDetailInfoDAL : IDataAccess<BfCodeDetailInfo>
    {
        int DeleteByBfCodeId(int bfCodeId);

        List<BfCodeDetailInfo> SelectBfCodeDetailByBfCodeIds(List<int> ids);
    }
}