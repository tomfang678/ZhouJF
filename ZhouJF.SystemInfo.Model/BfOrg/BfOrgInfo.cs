using System;
using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;
using System.Collections.Generic;

namespace YHPT.SystemInfo.Model.BfOrg
{
    [DataContract]
    [ModelMap("BF_ORG", "BfOrg", "ID")]
    [KnownType(typeof (BfOrgInfo))]
    public class BfOrgInfo : BaseModel
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
        [ColumnMap("OWNER_ID", DbType.Int32)]
        public int OWNER_ID { get; set; }

        [DataMember]
        [ColumnMap("OWNER_CODE", DbType.String)]
        public string OWNER_CODE { get; set; }

        [DataMember]
        [ColumnMap("OWNER_NAME", DbType.String)]
        public string OWNER_NAME { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USER", DbType.Int32)]
        public int LAST_MODIFIED_USER { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USERCODE", DbType.String)]
        public string LAST_MODIFIED_USERCODE { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_DATE", DbType.DateTime)]
        public DateTime LAST_MODIFIED_DATE { get; set; }

        public List<BfOrgInfo> CHILDS { get; set; }
    }
}