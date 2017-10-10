// 需要引用的命名空间  using System.Runtime.Serialization;  

using MB.Framework.Common;
using System;
/// <summary> 
/// 文件生成时间 2017-10-03 11:18 
/// </summary> 
using System.Runtime.Serialization;
namespace YHPT.SystemInfo.Model.YhManager
{
    public class ImgInfoDto : PagedQueryParam
    {
        public ImgInfoDto()
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
        private int? _module;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("module", System.Data.DbType.Int32)]
        public int? module
        {
            get { return _module; }
            set { _module = value; }
        }
        private string _imgModule;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("imgModule", System.Data.DbType.String)]
        public string imgModule
        {
            get { return _imgModule; }
            set { _imgModule = value; }
        }
        private string _imgUrl;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("imgUrl", System.Data.DbType.String)]
        public string imgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }
        private string _thembUrl;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("thembUrl", System.Data.DbType.String)]
        public string thembUrl
        {
            get { return _thembUrl; }
            set { _thembUrl = value; }
        }
        private string _remark;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("remark", System.Data.DbType.String)]
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private byte? _isDisable;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("isDisable", System.Data.DbType.String)]
        public byte? isDisable
        {
            get { return _isDisable; }
            set { _isDisable = value; }
        }
    }
}
