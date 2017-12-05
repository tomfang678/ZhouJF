// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2017-10-04 10:06  
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BridgeInfo", "Bridgeinfo", new string[] { "ID" })]
    [KnownType(typeof(BridgeInfo))]
    public class BridgeInfo : MB.Orm.Common.BaseModel
    {
        public BridgeInfo()
        {

        }

        private string _RoadName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadName", System.Data.DbType.String)]
        public string RoadName
        {
            get { return _RoadName; }
            set { _RoadName = value; }
        }
        private int? _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int? ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int? _RoadID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadID", System.Data.DbType.Int32)]
        public int? RoadID
        {
            get { return _RoadID; }
            set { _RoadID = value; }
        }
        private string _BridgeName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeName", System.Data.DbType.String)]
        public string BridgeName
        {
            get { return _BridgeName; }
            set { _BridgeName = value; }
        }
        private string _BridgeStructureStyle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeStructureStyle", System.Data.DbType.String)]
        public string BridgeStructureStyle
        {
            get { return _BridgeStructureStyle; }
            set { _BridgeStructureStyle = value; }
        }
        private string _InteractionAngle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("InteractionAngle", System.Data.DbType.String)]
        public string InteractionAngle
        {
            get { return _InteractionAngle; }
            set { _InteractionAngle = value; }
        }
        private string _BridgeSpanCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeSpanCount", System.Data.DbType.String)]
        public string BridgeSpanCount
        {
            get { return _BridgeSpanCount; }
            set { _BridgeSpanCount = value; }
        }
        private string _BridgeSpanStyle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeSpanStyle", System.Data.DbType.String)]
        public string BridgeSpanStyle
        {
            get { return _BridgeSpanStyle; }
            set { _BridgeSpanStyle = value; }
        }
        private double _BridgeSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeSquare", System.Data.DbType.Double)]
        public double BridgeSquare
        {
            get { return _BridgeSquare; }
            set { _BridgeSquare = value; }
        }
        private double _BridgeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeLength", System.Data.DbType.Double)]
        public double BridgeLength
        {
            get { return _BridgeLength; }
            set { _BridgeLength = value; }
        }
        private double _BridgeWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeWidth", System.Data.DbType.Double)]
        public double BridgeWidth
        {
            get { return _BridgeWidth; }
            set { _BridgeWidth = value; }
        }
        private double _DriveWayWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DriveWayWidth", System.Data.DbType.Double)]
        public double DriveWayWidth
        {
            get { return _DriveWayWidth; }
            set { _DriveWayWidth = value; }
        }
        private double _PavementWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PavementWidth", System.Data.DbType.Double)]
        public double PavementWidth
        {
            get { return _PavementWidth; }
            set { _PavementWidth = value; }
        }
        private string _MainBeamSize;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBeamSize", System.Data.DbType.String)]
        public string MainBeamSize
        {
            get { return _MainBeamSize; }
            set { _MainBeamSize = value; }
        }
        private string _MainBeamCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBeamCount", System.Data.DbType.String)]
        public string MainBeamCount
        {
            get { return _MainBeamCount; }
            set { _MainBeamCount = value; }
        }
        private string _BearingStyle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BearingStyle", System.Data.DbType.String)]
        public string BearingStyle
        {
            get { return _BearingStyle; }
            set { _BearingStyle = value; }
        }
        private string _BridgeRoadStructure;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeRoadStructure", System.Data.DbType.String)]
        public string BridgeRoadStructure
        {
            get { return _BridgeRoadStructure; }
            set { _BridgeRoadStructure = value; }
        }
        private string _ExpansionStyle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ExpansionStyle", System.Data.DbType.String)]
        public string ExpansionStyle
        {
            get { return _ExpansionStyle; }
            set { _ExpansionStyle = value; }
        }
        private int? _ExpansionCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ExpansionCount", System.Data.DbType.Int32)]
        public int? ExpansionCount
        {
            get { return _ExpansionCount; }
            set { _ExpansionCount = value; }
        }
        private string _MainBridgeLongitudinalSlope;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBridgeLongitudinalSlope", System.Data.DbType.String)]
        public string MainBridgeLongitudinalSlope
        {
            get { return _MainBridgeLongitudinalSlope; }
            set { _MainBridgeLongitudinalSlope = value; }
        }
        private string _MainBridgeCrossSlope;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBridgeCrossSlope", System.Data.DbType.String)]
        public string MainBridgeCrossSlope
        {
            get { return _MainBridgeCrossSlope; }
            set { _MainBridgeCrossSlope = value; }
        }
        private double _RailLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RailLength", System.Data.DbType.Double)]
        public double RailLength
        {
            get { return _RailLength; }
            set { _RailLength = value; }
        }
        private string _RailStructure;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RailStructure", System.Data.DbType.String)]
        public string RailStructure
        {
            get { return _RailStructure; }
            set { _RailStructure = value; }
        }
        private string _BankRevetmentStyle;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BankRevetmentStyle", System.Data.DbType.String)]
        public string BankRevetmentStyle
        {
            get { return _BankRevetmentStyle; }
            set { _BankRevetmentStyle = value; }
        }
        private string _CappingSize;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CappingSize", System.Data.DbType.String)]
        public string CappingSize
        {
            get { return _CappingSize; }
            set { _CappingSize = value; }
        }
        private string _PileFeature;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PileFeature", System.Data.DbType.String)]
        public string PileFeature
        {
            get { return _PileFeature; }
            set { _PileFeature = value; }
        }
        private string _WaterSupplyPipeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WaterSupplyPipeCount", System.Data.DbType.String)]
        public string WaterSupplyPipeCount
        {
            get { return _WaterSupplyPipeCount; }
            set { _WaterSupplyPipeCount = value; }
        }
        private string _GasPipeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("GasPipeCount", System.Data.DbType.String)]
        public string GasPipeCount
        {
            get { return _GasPipeCount; }
            set { _GasPipeCount = value; }
        }
        private string _ElectricPowerCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ElectricPowerCount", System.Data.DbType.String)]
        public string ElectricPowerCount
        {
            get { return _ElectricPowerCount; }
            set { _ElectricPowerCount = value; }
        }
        private string _CommsCableCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CommsCableCount", System.Data.DbType.String)]
        public string CommsCableCount
        {
            get { return _CommsCableCount; }
            set { _CommsCableCount = value; }
        }
        private string _Images;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Images", System.Data.DbType.String)]
        public string Images
        {
            get { return _Images; }
            set { _Images = value; }
        }
        private string _LeaderName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LeaderName", System.Data.DbType.String)]
        public string LeaderName
        {
            get { return _LeaderName; }
            set { _LeaderName = value; }
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

        private int? _SubContLeaderInfoID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContLeaderInfoID", System.Data.DbType.Int32)]
        public int? SubContLeaderInfoID
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
        private string _picture;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("picture", System.Data.DbType.String)]
        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

    }

}
