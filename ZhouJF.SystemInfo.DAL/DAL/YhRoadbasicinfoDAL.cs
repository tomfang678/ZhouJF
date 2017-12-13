
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;
namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("Roadbasicinfo", "Roadbasicinfo.xml")]
    public class YhRoadbasicinfoDAL : DALBaseSqlServer<Roadbasicinfo>, IYhRoadbasicinfoDAL
    {
        private static readonly string OBJECT_XML_FILE = "Roadbasicinfo";

        public YhRoadbasicinfoDAL()
            : base(typeof(YhRoadbasicinfoDAL))
        { }

        public bool CheckExistsMunicipalDetail(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("Roadmunicipalinfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count>0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }


        public bool CheckExistsSanitationDetail(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("Sanitationinfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckExistsSewerDetail(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("Sewerinfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckExistsGreenLandDetail(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("GreenLandInfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckExistsBridgeDetail(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("Bridgeinfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }


        public bool CheckExistsStreetInfo(int id)
        {
            using (var dbScope = new OperationDatabaseScope(ReadDb))
            {
                var ds = DatabaseExcuteByXmlHelper.NewInstance.GetDataSetByXml("StreetTreeInfo.xml",
                    "SelectRoadID", id);
                if (ds.Tables[0].Rows.Count > 0 && int.Parse(ds.Tables[0].Rows[0][0].ToString()) == 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

