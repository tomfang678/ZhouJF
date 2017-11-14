using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfModule
{
    [DataContract]
    [ModelMap("BF_MODULE_OPERATION", "BfModuleOperation", "ID")]
    [KnownType(typeof (BfModuleOperationInfo))]
    public class BfModuleOperationInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("BF_MODULE_ID", DbType.Int32)]
        public int BF_MODULE_ID { get; set; }

        [DataMember]
        [ColumnMap("CODE", DbType.String)]
        public string CODE { get; set; }

        [DataMember]
        [ColumnMap("MODULE_CODE", DbType.String)]
        public string MODULE_CODE { get; set; }

        public string MODULE_NAME
        {
            get
            {
                if (CODE.Equals("OPEN"))
                    return "浏览";
                else if(CODE.Equals("ADDNEW"))
                    return "新增";
                else if (CODE.Equals("DELETE"))
                    return "删除";
                return "更新";
            }
        }
    }
}