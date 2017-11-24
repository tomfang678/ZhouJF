// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhAreaInfo
{
    public class RoadbasicinfoDto : PagedQueryParam
    {
        public RoadbasicinfoDto()
        {

        }

        private string _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.String)]
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _RoadCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadCode", System.Data.DbType.String)]
        public string RoadCode
        {
            get { return _RoadCode; }
            set { _RoadCode = value; }
        }
        private string _RoadName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadName", System.Data.DbType.String)]
        public string RoadName
        {
            get { return _RoadName; }
            set { _RoadName = value; }
        }
        private string _RoadLevel;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadLevel", System.Data.DbType.String)]
        public string RoadLevel
        {
            get { return _RoadLevel; }
            set { _RoadLevel = value; }
        }
        private string _RoadMaterial;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadMaterial", System.Data.DbType.String)]
        public string RoadMaterial
        {
            get { return _RoadMaterial; }
            set { _RoadMaterial = value; }
        }
        private String _BridgeNumber;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeNumber", System.Data.DbType.String)]
        public String BridgeNumber
        {
            get { return _BridgeNumber; }
            set { _BridgeNumber = value; }
        }
        private string _AreaCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AreaCode", System.Data.DbType.String)]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
        }
        private int _AreaInfoID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AreaInfoID", System.Data.DbType.Int32)]
        public int AreaInfoID
        {
            get { return _AreaInfoID; }
            set { _AreaInfoID = value; }
        }


        private int? _SubContLeaderInfoID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContLeaderInfoID", System.Data.DbType.Int32)]
        public int? LeaderCode
        {
            get { return _SubContLeaderInfoID; }
            set { _SubContLeaderInfoID = value; }
        }

        private decimal _longitude;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("longitude", System.Data.DbType.Decimal)]
        public decimal longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private decimal _latitude;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("latitude", System.Data.DbType.Decimal)]
        public decimal latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }
        private string _v1;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("v1", System.Data.DbType.String)]
        public string v1
        {
            get { return _v1; }
            set { _v1 = value; }
        }
        private string _v2;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("v2", System.Data.DbType.String)]
        public string v2
        {
            get { return _v2; }
            set { _v2 = value; }
        }
        private string _v3;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("v3", System.Data.DbType.String)]
        public string v3
        {
            get { return _v3; }
            set { _v3 = value; }
        }
        private string _v4;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("v4", System.Data.DbType.String)]
        public string v4
        {
            get { return _v4; }
            set { _v4 = value; }
        }
        private string _v5;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("v5", System.Data.DbType.String)]
        public string v5
        {
            get { return _v5; }
            set { _v5 = value; }
        }

    }
}
