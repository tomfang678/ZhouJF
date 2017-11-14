using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.IDataAccess;
using System.Collections.Generic;
using YHPT.SystemInfo.Model.BfRegion;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfRegionInfoDAL : IDataAccess<BfRegionInfo>
    {
        int BatchImport(List<BfRegionInfo> entitys, DataBaseResource dbResource = DataBaseResource.Default);
    }
}
