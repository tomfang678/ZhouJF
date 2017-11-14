// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    public class SubContLeaderInfoDto : PagedQueryParam
    {
        public SubContLeaderInfoDto()
        {

        }

        private int? _SubContractorID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContractorID", System.Data.DbType.Int32)]
        public int? SubContractorID
        {
            get { return _SubContractorID; }
            set { _SubContractorID = value; }
        }
        private string _LeaderCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LeaderCode", System.Data.DbType.String)]
        public string LeaderCode
        {
            get { return _LeaderCode; }
            set { _LeaderCode = value; }
        }
        private string _LeaderName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LeaderName", System.Data.DbType.String)]
        public string LeaderName
        {
            get { return _LeaderName; }
            set { _LeaderName = value; }
        }
        private string _PhoneNumber;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PhoneNumber", System.Data.DbType.String)]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

    }
}
