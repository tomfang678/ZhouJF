using System.Collections.Generic;
using System.Text;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.DataAccess;
using MB.Orm.DB;
using MB.RuleBase.Common;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfCode;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.DAL
{
    [MB.Framework.RuleBase.ObjectDataMapping("BF_CODE_DETAIL", "BfCodeDetail.xml")]
    public class BfCodeDetailInfoDAL : DALBaseSqlServer<BfCodeDetailInfo>, IBfCodeDetailInfoDAL
    {
        public BfCodeDetailInfoDAL()
            : base(typeof(BfCodeDetailInfoDAL))
        { }

        public int DeleteByBfCodeId(int bfCodeId)
        {
            using (var dbScope = new OperationDatabaseScope(GetDbConnection(DataBaseResource.Default)))
            {
                var i = DatabaseExcuteByXmlHelper.NewInstance.ExecuteNonQuery("BfCodeDetail", "DeleteObjectByBfCodeId", bfCodeId);
                return i;
            }
        }

        public List<BfCodeDetailInfo> SelectBfCodeDetailByBfCodeIds(List<int> ids)
        {
            var result = new List<BfCodeDetailInfo>();
            var index = 0;
            var sb = new StringBuilder();
            foreach (var id in ids)
            {
                index++;
                sb.Append(id + ",");
                if (index > 950)
                {
                    result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfCodeDetailInfo>("BfCodeDetail", "SelectBfCodeDetailByBfCodeIds", sb.ToString().TrimEnd(',')));
                    index = 0;
                    sb = new StringBuilder();
                }
            }
            if (index > 0 && sb.ToString().Length > 0)
            {
                result.AddRange(DatabaseExcuteByXmlHelper.NewInstance.GetObjectsByXml<BfCodeDetailInfo>("BfCodeDetail", "SelectBfCodeDetailByBfCodeIds", sb.ToString().TrimEnd(',')));
            }
            return result;
        }
    }
}
