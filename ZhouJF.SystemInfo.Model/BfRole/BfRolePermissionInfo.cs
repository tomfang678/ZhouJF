using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfRole
{
    [DataContract]
    [ModelMap("BF_ROLE_MOD_PERMISSION", "BfRolePermission", "ID")]
    [KnownType(typeof (BfRolePermissionInfo))]
    public class BfRolePermissionInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("BF_ROLE_ID", DbType.Int32)]
        public int BF_ROLE_ID { get; set; }

        [DataMember]
        [ColumnMap("BF_MODULE_OPERATION_ID", DbType.Int32)]
        public int BF_MODULE_OPERATION_ID { get; set; }
    }
}