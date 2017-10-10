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
    /// 文件生成时间 2017-10-04 10:03 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("SewerInfo", "Sewerinfo", new string[] { "ID" })]
    [KnownType(typeof(SewerinfoInfo))]
    public class SewerinfoInfo : MB.Orm.Common.BaseModel
    {
        public SewerinfoInfo()
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
        private int? _ConnectingPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ConnectingPipeLength", System.Data.DbType.Int32)]
        public int? ConnectingPipeLength
        {
            get { return _ConnectingPipeLength; }
            set { _ConnectingPipeLength = value; }
        }
        private int? _SamllRainPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SamllRainPipeLength", System.Data.DbType.Int32)]
        public int? SamllRainPipeLength
        {
            get { return _SamllRainPipeLength; }
            set { _SamllRainPipeLength = value; }
        }
        private int? _MiddleRainPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("MiddleRainPipeLength", System.Data.DbType.Int32)]
        public int? MiddleRainPipeLength
        {
            get { return _MiddleRainPipeLength; }
            set { _MiddleRainPipeLength = value; }
        }
        private int? _BigRainPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BigRainPipeLength", System.Data.DbType.Int32)]
        public int? BigRainPipeLength
        {
            get { return _BigRainPipeLength; }
            set { _BigRainPipeLength = value; }
        }
        private int? _BiggerRainPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BiggerRainPipeLength", System.Data.DbType.Int32)]
        public int? BiggerRainPipeLength
        {
            get { return _BiggerRainPipeLength; }
            set { _BiggerRainPipeLength = value; }
        }
        private int? _DirtyWaterPipeLength;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DirtyWaterPipeLength", System.Data.DbType.Int32)]
        public int? DirtyWaterPipeLength
        {
            get { return _DirtyWaterPipeLength; }
            set { _DirtyWaterPipeLength = value; }
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
