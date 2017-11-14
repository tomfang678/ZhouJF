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
        private int? _BridgeSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeSquare", System.Data.DbType.Int32)]
        public int? BridgeSquare
        {
            get { return _BridgeSquare; }
            set { _BridgeSquare = value; }
        }
        private int? _BridgeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeLength", System.Data.DbType.Int32)]
        public int? BridgeLength
        {
            get { return _BridgeLength; }
            set { _BridgeLength = value; }
        }
        private int? _BridgeWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeWidth", System.Data.DbType.Int32)]
        public int? BridgeWidth
        {
            get { return _BridgeWidth; }
            set { _BridgeWidth = value; }
        }
        private int? _DriveWayWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DriveWayWidth", System.Data.DbType.Int32)]
        public int? DriveWayWidth
        {
            get { return _DriveWayWidth; }
            set { _DriveWayWidth = value; }
        }
        private int? _PavementWidth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PavementWidth", System.Data.DbType.Int32)]
        public int? PavementWidth
        {
            get { return _PavementWidth; }
            set { _PavementWidth = value; }
        }
        private int? _MainBeamSize;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBeamSize", System.Data.DbType.Int32)]
        public int? MainBeamSize
        {
            get { return _MainBeamSize; }
            set { _MainBeamSize = value; }
        }
        private int? _MainBeamCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBeamCount", System.Data.DbType.Int32)]
        public int? MainBeamCount
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
        private double? _MainBridgeLongitudinalSlope;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBridgeLongitudinalSlope", System.Data.DbType.String)]
        public double? MainBridgeLongitudinalSlope
        {
            get { return _MainBridgeLongitudinalSlope; }
            set { _MainBridgeLongitudinalSlope = value; }
        }
        private double? _MainBridgeCrossSlope;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MainBridgeCrossSlope", System.Data.DbType.String)]
        public double? MainBridgeCrossSlope
        {
            get { return _MainBridgeCrossSlope; }
            set { _MainBridgeCrossSlope = value; }
        }
        private int? _RailLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RailLength", System.Data.DbType.Int32)]
        public int? RailLength
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
        private int? _CappingSize;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CappingSize", System.Data.DbType.Int32)]
        public int? CappingSize
        {
            get { return _CappingSize; }
            set { _CappingSize = value; }
        }
        private int? _PileFeature;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PileFeature", System.Data.DbType.Int32)]
        public int? PileFeature
        {
            get { return _PileFeature; }
            set { _PileFeature = value; }
        }
        private int? _WaterSupplyPipeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WaterSupplyPipeCount", System.Data.DbType.Int32)]
        public int? WaterSupplyPipeCount
        {
            get { return _WaterSupplyPipeCount; }
            set { _WaterSupplyPipeCount = value; }
        }
        private int? _GasPipeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("GasPipeCount", System.Data.DbType.Int32)]
        public int? GasPipeCount
        {
            get { return _GasPipeCount; }
            set { _GasPipeCount = value; }
        }
        private int? _ElectricPowerCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ElectricPowerCount", System.Data.DbType.Int32)]
        public int? ElectricPowerCount
        {
            get { return _ElectricPowerCount; }
            set { _ElectricPowerCount = value; }
        }
        private int? _CommsCableCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CommsCableCount", System.Data.DbType.Int32)]
        public int? CommsCableCount
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
    }

}
