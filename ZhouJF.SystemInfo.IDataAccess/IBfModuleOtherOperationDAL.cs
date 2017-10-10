using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfModuleOtherOperationDAL : IDataAccess<BfModuleOtherOperationInfo>
    {
        int DeleteByModuleID(int moduleID, DataBaseResource dbResource = DataBaseResource.Default);

        List<BfModuleOtherOperationInfo> SelectOtherOperationByModule(List<int> moduleIds);
    }
}
