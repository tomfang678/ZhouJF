﻿// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18  
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    public class SewerinfoDto : PagedQueryParam
    {
        public SewerinfoDto()
        {

        }
        private int? _RoadID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadID", System.Data.DbType.Int32)]
        public int? RoadID
        {
            get { return _RoadID; }
            set { _RoadID = value; }
        }
        private string _RoadName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadName", System.Data.DbType.String)]
        public string RoadName
        {
            get { return _RoadName; }
            set { _RoadName = value; }
        }
        private string _LeaderCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LeaderCode", System.Data.DbType.String)]
        public string LeaderCode
        {
            get { return _LeaderCode; }
            set { _LeaderCode = value; }
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
        private string _RoadBetween;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadBetween", System.Data.DbType.String)]
        public string RoadBetween
        {
            get { return _RoadBetween; }
            set { _RoadBetween = value; }
        }
        private string _RainPipeDeepth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RainPipeDeepth", System.Data.DbType.String)]
        public string RainPipeDeepth
        {
            get { return _RainPipeDeepth; }
            set { _RainPipeDeepth = value; }
        }
        private string _DirtyWaterPipeDeepth;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DirtyWaterPipeDeepth", System.Data.DbType.String)]
        public string DirtyWaterPipeDeepth
        {
            get { return _DirtyWaterPipeDeepth; }
            set { _DirtyWaterPipeDeepth = value; }
        }
        private int _RainInspectionShaftCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RainInspectionShaftCount", System.Data.DbType.String)]
        public int RainInspectionShaftCount
        {
            get { return _RainInspectionShaftCount; }
            set { _RainInspectionShaftCount = value; }
        }
        private int _DiryWaternspectionShaftCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DiryWaternspectionShaftCount", System.Data.DbType.String)]
        public int DiryWaternspectionShaftCount
        {
            get { return _DiryWaternspectionShaftCount; }
            set { _DiryWaternspectionShaftCount = value; }
        }
        private int _WaterInletCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("WaterInletCount", System.Data.DbType.String)]
        public int WaterInletCount
        {
            get { return _WaterInletCount; }
            set { _WaterInletCount = value; }
        }
    }
}
