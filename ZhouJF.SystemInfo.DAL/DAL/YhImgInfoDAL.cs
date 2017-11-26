
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("ImgInfo", "Imginfo.xml")]
    public class YhImgInfoDAL : DALBaseSqlServer<ImgInfo>, IYhImgInfoDAL
    {
        public YhImgInfoDAL()
            : base(typeof(YhImgInfoDAL))
        { }

        public System.Collections.Generic.List<ImgInfo> GetImgByModuleId(string module, int moduleId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<ImgInfo>("Imginfo.xml",
                    "GetImgByModuleId",moduleId,module);
                return result;
            }
        }
        public System.Collections.Generic.List<ImgInfo> GetImgByRoadId(int RoadId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<ImgInfo>("Imginfo.xml",
                    "GetImgByRoadId", RoadId);
                return result;
            }
        }
        public System.Collections.Generic.List<ImgInfo> GetImgByModuleIdAndType(int moduleId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<ImgInfo>("Imginfo",
                    "GetImgByModuleIdAndType", moduleId);
                return result;
            }
        }

        public ImgInfo GetImgById(int moduleId)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var result = DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<ImgInfo>("Imginfo",
                    "GetImgById", moduleId);
                if (result.Count > 0)
                {
                    return result[0];
                }
                return null;
            }
        }

        public int DeleteByModuleId(int moduleId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("Imginfo", "DeleteByModuleId", moduleId);
                return i;
            }
        }

        public int DeleteByImgId(int imgId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("Imginfo", "DeleteByImgId", imgId);
                return i;
            }
        }

        public int updateByImgId(ImgInfo img)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("Imginfo", "updateByImgId", img);
                return i;
            }
        }
    }
}

