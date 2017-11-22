// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhRoadMunicipalInfo
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("RoadMunicipalInfo", "Roadmunicipalinfo", new string[] { "ID" })]
    [KnownType(typeof(RoadMunicipalInfo))]
    public class RoadMunicipalInfo : MB.Orm.Common.BaseModel
    {

        public RoadMunicipalInfo()
        {

        }
        private int _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _RoadID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadID", System.Data.DbType.Int32)]
        public int RoadID
        {
            get { return _RoadID; }
            set { _RoadID = value; }
        }
        private int? _RoadLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadLength", System.Data.DbType.Int32)]
        public int? RoadLength
        {
            get { return _RoadLength; }
            set { _RoadLength = value; }
        }
        private int? _RoadSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadSquare", System.Data.DbType.Int32)]
        public int? RoadSquare
        {
            get { return _RoadSquare; }
            set { _RoadSquare = value; }
        }
        private int? _SlowLaneWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SlowLaneWidth", System.Data.DbType.Int32)]
        public int? SlowLaneWidth
        {
            get { return _SlowLaneWidth; }
            set { _SlowLaneWidth = value; }
        }
        private int? _SlowLaneSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SlowLaneSquare", System.Data.DbType.Int32)]
        public int? SlowLaneSquare
        {
            get { return _SlowLaneSquare; }
            set { _SlowLaneSquare = value; }
        }
        private int? _FastLaneWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FastLaneWidth", System.Data.DbType.Int32)]
        public int? FastLaneWidth
        {
            get { return _FastLaneWidth; }
            set { _FastLaneWidth = value; }
        }
        private int? _FastLaneSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FastLaneSquare", System.Data.DbType.Int32)]
        public int? FastLaneSquare
        {
            get { return _FastLaneSquare; }
            set { _FastLaneSquare = value; }
        }
        private int? _PavementWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PavementWidth", System.Data.DbType.Int32)]
        public int? PavementWidth
        {
            get { return _PavementWidth; }
            set { _PavementWidth = value; }
        }
        private int? _PavementSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PavementSquare", System.Data.DbType.Int32)]
        public int? PavementSquare
        {
            get { return _PavementSquare; }
            set { _PavementSquare = value; }
        }
        private int? _CurbSideLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CurbSideLength", System.Data.DbType.Int32)]
        public int? CurbSideLength
        {
            get { return _CurbSideLength; }
            set { _CurbSideLength = value; }
        }
        private int? _CurbFlatLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CurbFlatLength", System.Data.DbType.Int32)]
        public int? CurbFlatLength
        {
            get { return _CurbFlatLength; }
            set { _CurbFlatLength = value; }
        }
        private int? _InspectionShaftCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("InspectionShaftCount", System.Data.DbType.Int32)]
        public int? InspectionShaftCount
        {
            get { return _InspectionShaftCount; }
            set { _InspectionShaftCount = value; }
        }
        private int? _WaterInletCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WaterInletCount", System.Data.DbType.Int32)]
        public int? WaterInletCount
        {
            get { return _WaterInletCount; }
            set { _WaterInletCount = value; }
        }
        private int? _WaterOutletCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WaterOutletCount", System.Data.DbType.Int32)]
        public int? WaterOutletCount
        {
            get { return _WaterOutletCount; }
            set { _WaterOutletCount = value; }
        }
        private int? _GuideBoardCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("GuideBoardCount", System.Data.DbType.Int32)]
        public int? GuideBoardCount
        {
            get { return _GuideBoardCount; }
            set { _GuideBoardCount = value; }
        }
        private int? _PersonDivideLine;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PersonDivideLine", System.Data.DbType.Int32)]
        public int? PersonDivideLine
        {
            get { return _PersonDivideLine; }
            set { _PersonDivideLine = value; }
        }
        private int? _VehicleDivideLine;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VehicleDivideLine", System.Data.DbType.Int32)]
        public int? VehicleDivideLine
        {
            get { return _VehicleDivideLine; }
            set { _VehicleDivideLine = value; }
        }
        private int? _VehicleBicycleDivideLine;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("VehicleBicycleDivideLine", System.Data.DbType.Int32)]
        public int? VehicleBicycleDivideLine
        {
            get { return _VehicleBicycleDivideLine; }
            set { _VehicleBicycleDivideLine = value; }
        }
        private string _LeaderCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LeaderCode", System.Data.DbType.String)]
        public string LeaderCode
        {
            get { return _LeaderCode; }
            set { _LeaderCode = value; }
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
        private string _pricture;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("pricture", System.Data.DbType.String)]
        public string pricture
        {
            get { return _pricture; }
            set { _pricture = value; }
        }

        private string _RoadName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadName", System.Data.DbType.String)]
        public string RoadName
        {
            get { return _RoadName; }
            set { _RoadName = value; }
        }

        private int? _SubContLeaderInfoID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContLeaderInfoID", System.Data.DbType.Int32)]
        public int? SubContLeaderInfoID
        {
            get { return _SubContLeaderInfoID; }
            set { _SubContLeaderInfoID = value; }
        }
        private string _longitude;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("longitude", System.Data.DbType.String)]
        public string longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        private string _latitude;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("latitude", System.Data.DbType.String)]
        public string latitude
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