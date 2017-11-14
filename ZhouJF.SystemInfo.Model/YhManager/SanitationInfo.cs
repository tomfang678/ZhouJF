﻿// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  

    /// <summary> 
    /// 文件生成时间 2017-10-04 10:55 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("SanitationInfo ", "Sanitationinfo ", new string[] { "ID" })]
    [KnownType(typeof(SanitationInfo))]
    public class SanitationInfo : MB.Orm.Common.BaseModel
    {

        public SanitationInfo()
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
        private int? _MachineCleanLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MachineCleanLength", System.Data.DbType.Int32)]
        public int? MachineCleanLength
        {
            get { return _MachineCleanLength; }
            set { _MachineCleanLength = value; }
        }
        private int? _MachineWashLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MachineWashLength", System.Data.DbType.Int32)]
        public int? MachineWashLength
        {
            get { return _MachineWashLength; }
            set { _MachineWashLength = value; }
        }
        private int? _ManualCleanSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ManualCleanSquare", System.Data.DbType.Int32)]
        public int? ManualCleanSquare
        {
            get { return _ManualCleanSquare; }
            set { _ManualCleanSquare = value; }
        }
        private int? _ManualWashSquare;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ManualWashSquare", System.Data.DbType.Int32)]
        public int? ManualWashSquare
        {
            get { return _ManualWashSquare; }
            set { _ManualWashSquare = value; }
        }
        private int? _ManualQuota;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ManualQuota", System.Data.DbType.Int32)]
        public int? ManualQuota
        {
            get { return _ManualQuota; }
            set { _ManualQuota = value; }
        }
        private int? _DustbinCount;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DustbinCount", System.Data.DbType.Int32)]
        public int? DustbinCount
        {
            get { return _DustbinCount; }
            set { _DustbinCount = value; }
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