using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.Model.BfRole
{
    [DataContract]
    [ModelMap("BF_ROLE", "BfRole", "ID")]
    [KnownType(typeof(BfRoleInfo))]
    public class BfRoleInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("NAME", DbType.String)]
        public string NAME { get; set; }

        [DataMember]
        [ColumnMap("CODE", DbType.String)]
        public string CODE { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USER", DbType.Int32)]
        public int LAST_MODIFIED_USER { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USERCODE", DbType.String)]
        public string LAST_MODIFIED_USERCODE { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_DATE", DbType.DateTime)]
        public DateTime LAST_MODIFIED_DATE { get; set; }

        [DataMember]
        [ColumnMap("REMARK", DbType.String)]
        public string REMARK { get; set; }

        [DataMember]
        [ColumnMap("IS_ADMIN", DbType.Int32)]
        public int IS_ADMIN { get; set; }

        public string ISADMIN
        {
            get
            {
                if (IS_ADMIN > 0)
                {
                    return "是";
                }
                return "否";
            }
        }

        /// <summary>
        ///     普通模块操作
        /// </summary>
        [DataMember]
        public List<BfRolePermissionInfo> OperationItems { get; set; }

        /// <summary>
        ///     其他模块操作
        /// </summary>
        [DataMember]
        public List<BfRoleSpecialPermissionInfo> OtherOperationItems { get; set; }
    }
}