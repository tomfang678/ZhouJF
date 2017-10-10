using System;
using System.Runtime.Serialization;

namespace YHPT.SystemInfo.Model.BfRoleUser
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BF_ROLE_USER", "BfRoleUser", new string[] {"ID"})]
    [KnownType(typeof (BfRoleUserInfo))]
    public class BfRoleUserInfo : MB.Orm.Common.BaseModel
    {

        public BfRoleUserInfo()
        {

        }

        private int? _ID;

        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private int? _BF_USER_ID;

        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BF_USER_ID", System.Data.DbType.Int32)]
        public int? BF_USER_ID
        {
            get { return _BF_USER_ID; }
            set { _BF_USER_ID = value; }
        }

        private int? _BF_ROLE_ID;

        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BF_ROLE_ID", System.Data.DbType.Int32)]
        public int? BF_ROLE_ID
        {
            get { return _BF_ROLE_ID; }
            set { _BF_ROLE_ID = value; }
        }

        private int? _LAST_MODIFIED_USER;

        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_USER", System.Data.DbType.Int32)]
        public int? LAST_MODIFIED_USER
        {
            get { return _LAST_MODIFIED_USER; }
            set { _LAST_MODIFIED_USER = value; }
        }

        private DateTime? _LAST_MODIFIED_DATE;

        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_DATE", System.Data.DbType.DateTime)]
        public DateTime? LAST_MODIFIED_DATE
        {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }
    }
}