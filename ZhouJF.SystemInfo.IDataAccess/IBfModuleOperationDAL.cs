using System.Collections.Generic;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfModuleOperationDAL : IDataAccess<BfModuleOperationInfo>
    {
        int DeleteByModuleID(int moduleID, DataBaseResource dbResource = DataBaseResource.Default);

        List<BfModuleOperationInfo> SelectOperationByUserID(int UserID);

        List<BfModuleOperationInfo> SelectOperationByModule(List<int> moduleIds);

    }
}
