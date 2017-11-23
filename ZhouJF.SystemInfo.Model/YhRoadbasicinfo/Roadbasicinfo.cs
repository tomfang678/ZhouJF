// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
using System.Collections.Generic;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;
namespace YHPT.SystemInfo.Model.YhRoadbasicinfo
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("Roadbasicinfo", "Roadbasicinfo", new string[] { "ID" })]
    [KnownType(typeof(Roadbasicinfo))]
    public class Roadbasicinfo : MB.Orm.Common.BaseModel
    {
        public Roadbasicinfo()
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
        private string _BridgeNumber;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BridgeNumber", System.Data.DbType.String)]
        public string BridgeNumber
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

        private string _Area;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Area", System.Data.DbType.String)]
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }


        private int? _SubContLeaderInfoID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SubContLeaderInfoID", System.Data.DbType.Int32)]
        public int? LeaderCode
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


        /// <summary>
        /// 市政
        /// </summary>
        public List<RoadMunicipalInfo> RoadMunicipalInfoList { get; set; }
        /// <summary>
        /// 环卫
        /// </summary>
        public List<SanitationInfo> SanitationInfoList { get; set; }
        /// <summary>
        /// 绿化
        /// </summary>
        public List<GreenLandInfo> GenLandInfoList { get; set; }
        /// <summary>
        /// 下水道
        /// </summary>
        public List<SewerinfoInfo> SewerinfoInfoList { get; set; }
        /// <summary>
        /// 桥梁
        /// </summary>
        public List<BridgeInfo> BridgeInfoList { get; set; }
        /// <summary>
        /// 行道树
        /// </summary>
        public List<StreetTreeInfo> StreetTreeInfoList { get; set; }
    }
}
