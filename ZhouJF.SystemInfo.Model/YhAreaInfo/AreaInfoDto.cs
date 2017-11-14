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
    }
}
