// 需要引用的命名空间  using System.ServiceModel; 
using MB.Framework.RuleBase.IDataAccess;
using System.Collections.Generic;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhManager;


namespace YHPT.SystemInfo.IDataAccess
{
    /// <summary> 
    /// 文件生成时间 2017-10-03 11:18 
    /// </summary> 
    [ServiceContract]
    public interface IYhImgInfoDAL : IDataAccess<ImgInfo>
    {
        List<ImgInfo> GetImgByModuleId(int moduleId);
        List<ImgInfo> GetImgByModuleIdAndType(int moduleId);
        ImgInfo GetImgById(int moduleId);
        int DeleteByModuleId(int moduleId);
        int DeleteByImgId(int imgId);
        int updateByImgId(ImgInfo img);
    }
}