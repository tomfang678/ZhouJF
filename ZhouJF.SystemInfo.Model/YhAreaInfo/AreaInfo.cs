﻿// 需要引用的命名空间  using System.Runtime.Serialization;  

using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhAreaInfo
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("AreaInfo", "Areainfo", new string[] { "ID" })]
    [KnownType(typeof(AreaInfo))]
    public class AreaInfo : MB.Orm.Common.BaseModel
    {
        public AreaInfo()
        {

        }
        private int? _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int? ID
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