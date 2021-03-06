﻿using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    /// <summary> 
    /// 文件生成时间 2017-10-04 10:52  
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("GreenLandInfo", "Greenlandinfo", new string[] { "ID" })]
    [KnownType(typeof(GreenLandInfo))]
    public class GreenLandInfo : MB.Orm.Common.BaseModel
    {

        public GreenLandInfo()
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
        private int? _FirstLevelSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FirstLevelSquare", System.Data.DbType.Int32)]
        public int? FirstLevelSquare
        {
            get { return _FirstLevelSquare; }
            set { _FirstLevelSquare = value; }
        }
        private int? _SecondLevelSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SecondLevelSquare", System.Data.DbType.Int32)]
        public int? SecondLevelSquare
        {
            get { return _SecondLevelSquare; }
            set { _SecondLevelSquare = value; }
        }
        private int? _ThirdLevelSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ThirdLevelSquare", System.Data.DbType.Int32)]
        public int? ThirdLevelSquare
        {
            get { return _ThirdLevelSquare; }
            set { _ThirdLevelSquare = value; }
        }
        private int? _FlowerSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("FlowerSquare", System.Data.DbType.Int32)]
        public int? FlowerSquare
        {
            get { return _FlowerSquare; }
            set { _FlowerSquare = value; }
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
        private string _picture;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("picture", System.Data.DbType.String)]
        public string picture
        {
            get { return _picture; }
            set { _picture = value; }
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
    }
}
