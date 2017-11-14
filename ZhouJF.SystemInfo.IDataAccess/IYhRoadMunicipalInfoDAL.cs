using MB.Framework.Common.Enums;
// 需要引用的命名空间  using System.ServiceModel; 
using MB.Framework.RuleBase.IDataAccess;
using System.Collections.Generic;
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;


namespace YHPT.SystemInfo.IDataAccess
{
    /// <summary> 
    /// 文件生成时间 2017-10-03 11:18 
    /// </summary> 
    [ServiceContract]
    public interface IYhRoadMunicipalInfoDAL : IDataAccess<RoadMunicipalInfo>
    {
        List<RoadMunicipalInfo> getByRoadId(int roadId);
    }
}