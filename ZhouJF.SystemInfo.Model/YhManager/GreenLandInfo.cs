using System;
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
        private string _GreenLandCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("GreenLandCode", System.Data.DbType.String)]
        public string GreenLandCode
        {
            get { return _GreenLandCode; }
            set { _GreenLandCode = value; }
        }
        private string _GreenLandName;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("GreenLandName", System.Data.DbType.String)]
        public string GreenLandName
        {
            get { return _GreenLandName; }
            set { _GreenLandName = value; }
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
