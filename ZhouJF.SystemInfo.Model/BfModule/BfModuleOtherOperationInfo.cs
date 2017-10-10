using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfModule
{
    [DataContract]
    [ModelMap("BF_MODULE_OTHER_OPERATION", "BfModuleOtherOperation", "ID")]
    [KnownType(typeof (BfModuleOtherOperationInfo))]
    public class BfModuleOtherOperationInfo : BaseModel
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
        [ColumnMap("NAME", DbType.String)]
        public string NAME { get; set; }
    }
}