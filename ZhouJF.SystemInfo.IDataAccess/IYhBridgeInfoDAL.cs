// 需要引用的命名空间  using System.ServiceModel; 
using MB.Framework.RuleBase.IDataAccess;
using System.Collections.Generic;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;


namespace YHPT.SystemInfo.IDataAccess
{
    /// <summary> 
    /// 文件生成时间 2017-10-03 11:18 
    /// </summary> 
    [ServiceContract]
    public interface IYhBridgeInfoDAL : IDataAccess<BridgeInfo>
    {
        List<BridgeInfo> getByRoadId(int roadId);
    }
}