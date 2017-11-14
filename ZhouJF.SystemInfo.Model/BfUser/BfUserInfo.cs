using System;
using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfUser
{
    [DataContract]
    [ModelMap("BF_USER", "BfUser", "ID")]
    [KnownType(typeof (BfUserInfo))]
    public class BfUserInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("ID", DbType.Int32)]
        public int ID { get; set; }

        [DataMember]
        [ColumnMap("CODE", DbType.String)]
        public string CODE { get; set; }

        [DataMember]
        [ColumnMap("NAME", DbType.String)]
        public string NAME { get; set; }

        [DataMember]
        [ColumnMap("OWNER_ID", DbType.Int32)]
        public int? OWNER_ID { get; set; }

        [DataMember]
        [ColumnMap("EXPIRE_TIME", DbType.DateTime)]
        public DateTime? EXPIRE_TIME { get; set; }

        [DataMember]
        [ColumnMap("PASSWORD", DbType.String)]
        public string PASSWORD { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_DATE", DbType.DateTime)]
        public DateTime? LAST_MODIFIED_DATE { get; set; }

        [DataMember]
        [ColumnMap("LAST_CHANGPWD_DATE", DbType.DateTime)]
        public DateTime? LAST_CHANGPWD_DATE { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USER", DbType.Int32)]
        public int? LAST_MODIFIED_USER { get; set; }

        [DataMember]
        [ColumnMap("LAST_MODIFIED_USERCODE", DbType.String)]
        public string LAST_MODIFIED_USERCODE { get; set; }

        [DataMember]
        [ColumnMap("OWNER_CODE", DbType.String)]
        public string OWNER_CODE { get; set; }

        [DataMember]
        [ColumnMap("ORG_NAME", DbType.String)]
        public string ORG_NAME { get; set; }

        [DataMember]
        [ColumnMap("LEADERID", DbType.Int32)]
        public int? LEADERID { get; set; }

        [DataMember]
        [ColumnMap("JOB", DbType.Int32)]
        public int? JOB { get; set; }
    }
}