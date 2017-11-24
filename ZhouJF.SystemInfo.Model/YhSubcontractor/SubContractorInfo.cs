// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.Subcontractor
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("SubContractorInfo", "SubContractorInfo", new string[] { "ID" })]
    [KnownType(typeof(SubContractorInfo))]
    public class SubContractorInfo : MB.Orm.Common.BaseModel
    {
        public SubContractorInfo()
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
        private string _SubContractorCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContractorCode", System.Data.DbType.String)]
        public string SubContractorCode
        {
            get { return _SubContractorCode; }
            set { _SubContractorCode = value; }
        }
        private string _SubContractorCrop;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContractorCrop", System.Data.DbType.String)]
        public string SubContractorCrop
        {
            get { return _SubContractorCrop; }
            set { _SubContractorCrop = value; }
        }
        private string _SubContractorBoss;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContractorBoss", System.Data.DbType.String)]
        public string SubContractorBoss
        {
            get { return _SubContractorBoss; }
            set { _SubContractorBoss = value; }
        }
        private string _PhoneNumber;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PhoneNumber", System.Data.DbType.String)]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _CreateUser;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CreateUser", System.Data.DbType.String)]
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }
        private DateTime? _CreateTime;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CreateTime", System.Data.DbType.DateTime)]
        public DateTime? CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _LastModifiedUser;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LastModifiedUser", System.Data.DbType.String)]
        public string LastModifiedUser
        {
            get { return _LastModifiedUser; }
            set { _LastModifiedUser = value; }
        }
        private DateTime? _LastModifiedTime;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LastModifiedTime", System.Data.DbType.DateTime)]
        public DateTime? LastModifiedTime
        {
            get { return _LastModifiedTime; }
            set { _LastModifiedTime = value; }
        }
    }
}
