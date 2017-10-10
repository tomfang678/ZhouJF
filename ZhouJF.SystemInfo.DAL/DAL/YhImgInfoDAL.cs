
using MB.Framework.RuleBase.DataAccess;
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

    }
}

