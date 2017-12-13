using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18  
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    /// <summary> 
    /// 文件生成时间 2017-11-22 10:33 
    /// </summary> 
    public class StreetTreeDtlInfoDto : PagedQueryParam
    {
        public StreetTreeDtlInfoDto()
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
        private int? _RoadID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("RoadID", System.Data.DbType.Int32)]
        public int? RoadID
        {
            get { return _RoadID; }
            set { _RoadID = value; }
        }
        private string _Code;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Code", System.Data.DbType.String)]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        private string _Type;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Type", System.Data.DbType.String)]
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int? _Size;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Size", System.Data.DbType.Int32)]
        public int? Size
        {
            get { return _Size; }
            set { _Size = value; }
        }
        private string _Status;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Status", System.Data.DbType.String)]
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private bool? _IsDisturbPeople;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("IsDisturbPeople", System.Data.DbType.Boolean)]
        public bool? IsDisturbPeople
        {
            get { return _IsDisturbPeople; }
            set { _IsDisturbPeople = value; }
        }
        private string _CoverBoardType;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CoverBoardType", System.Data.DbType.String)]
        public string CoverBoardType
        {
            get { return _CoverBoardType; }
            set { _CoverBoardType = value; }
        }
        private string _Specification;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Specification", System.Data.DbType.String)]
        public string Specification
        {
            get { return _Specification; }
            set { _Specification = value; }
        }
        private string _Protect;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Protect", System.Data.DbType.String)]
        public string Protect
        {
            get { return _Protect; }
            set { _Protect = value; }
        }
        private string _Remark;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Remark", System.Data.DbType.String)]
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _Picture;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("Picture", System.Data.DbType.String)]
        public string Picture
        {
            get { return _Picture; }
            set { _Picture = value; }
        }
    }
}
