using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfRole
{
    [DataContract]
    [ModelMap("BF_ROLE_MOD_SPECIAL_PERMISSION", "BfRoleSpecialPermission", "ID")]
    [KnownType(typeof (BfRoleSpecialPermissionInfo))]
    public class BfRoleSpecialPermissionInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("BF_ROLE_ID", DbType.Int32)]
        public int BF_ROLE_ID { get; set; }

        [DataMember]
        [ColumnMap("BF_MODULE_SPECIAL_ID", DbType.Int32)]
        public int BF_MODULE_SPECIAL_ID { get; set; }
    }
}