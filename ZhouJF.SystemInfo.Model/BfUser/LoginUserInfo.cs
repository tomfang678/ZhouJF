using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfUser
{
    [ModelMap("BF_USER", "BF_USER", "ID")]
    [DataContract]
    public class LoginUserInfo : BaseModel
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
        [ColumnMap("ACCESSTOKEN", DbType.String)]
        public string ACCESSTOKEN { get; set; }
    }
}