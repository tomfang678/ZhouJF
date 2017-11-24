// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhAreaInfo
{
    public class AreaInfoDto : PagedQueryParam
    {
        public AreaInfoDto()
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
        private string _Owner;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Owner", System.Data.DbType.String)]
        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }
        private string _Section;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Section", System.Data.DbType.String)]
        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }
        private string _Dept;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Dept", System.Data.DbType.String)]
        public string Dept
        {
            get { return _Dept; }
            set { _Dept = value; }
        }
        private string _Area;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Area", System.Data.DbType.String)]
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
        private string _AreaCode;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("AreaCode", System.Data.DbType.String)]
        public string AreaCode
        {
            get { return _AreaCode; }
            set { _AreaCode = value; }
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

        private string _region;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("region", System.Data.DbType.String)]
        public string region
        {
            get { return _region; }
            set { _region = value; }
        }

        public int HouseResourseType { get; set; }
        public int RentRange { get; set; }
        public int HousingType { get; set; }
        public int OccupancyType { get; set; }
        public int AreaRange { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public decimal fromlng { get; set; }
        public decimal tolng { get; set; }
        public decimal fromlat { get; set; }
        public decimal tolat { get; set; }
        public decimal price { get; set; }
    }
}
