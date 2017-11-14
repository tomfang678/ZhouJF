using System.Collections.Generic;
using MB.Framework.RuleBase.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.IDataAccess
{
    public interface IBfModuleDAL : IDataAccess<BfModuleInfo>
    {
        List<BfModuleInfo> SelectObjectByUserID(int UserID);

        List<BfModuleInfo> SelectAllChildBfModuleInfos();
    }
}
