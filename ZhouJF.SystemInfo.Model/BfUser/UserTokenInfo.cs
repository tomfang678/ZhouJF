using System;
using System.Data;
using System.Runtime.Serialization;
using MB.Orm.Common;
using MB.Orm.Mapping.Att;

namespace YHPT.SystemInfo.Model.BfUser
{
    [ModelMap("UserToken", new[] {"USERID"})]
    [DataContract]
    public class UserTokenInfo : BaseModel
    {
        [DataMember]
        [ColumnMap("USERID", DbType.Int32)]
        public int USERID { get; set; }

        [DataMember]
        [ColumnMap("TOKEN", DbType.String)]
        public string TOKEN { get; set; }

        [DataMember]
        [ColumnMap("CREATE_TIME", DbType.DateTime)]
        public DateTime CREATE_TIME { get; set; }
    }
}