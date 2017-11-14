﻿using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    /// <summary> 
    /// 文件生成时间 2017-10-04 10:56 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("StreetTreeInfo  ", "StreetTreeInfo  ", new string[] { "ID" })]
    [KnownType(typeof(StreetTreeInfo))]
    public class StreetTreeInfo : MB.Orm.Common.BaseModel
    {
        public StreetTreeInfo()
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
        private int? _SamllTreeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SamllTreeCount", System.Data.DbType.Int32)]
        public int? SamllTreeCount
        {
            get { return _SamllTreeCount; }
            set { _SamllTreeCount = value; }
        }
        private int? _MiddleTreeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MiddleTreeCount", System.Data.DbType.Int32)]
        public int? MiddleTreeCount
        {
            get { return _MiddleTreeCount; }
            set { _MiddleTreeCount = value; }
        }
        private int? _BigTreeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BigTreeCount", System.Data.DbType.Int32)]
        public int? BigTreeCount
        {
            get { return _BigTreeCount; }
            set { _BigTreeCount = value; }
        }
        private int? _BiggerTreeCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BiggerTreeCount", System.Data.DbType.Int32)]
        public int? BiggerTreeCount
        {
            get { return _BiggerTreeCount; }
            set { _BiggerTreeCount = value; }
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
    }
}