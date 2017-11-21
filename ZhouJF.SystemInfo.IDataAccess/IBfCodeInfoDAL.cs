using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfCodeInfoDAL : IDataAccess<BfCodeInfo>
    {
        int BatchImport(List<BfCodeInfo> entitys, DataBaseResource dbResource = DataBaseResource.Default);

        List<BfCodeDetailInfo> GetBfCodeByModuleId(string moduleId);

    }
}