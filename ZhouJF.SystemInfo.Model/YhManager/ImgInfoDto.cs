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

        private String _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.String)]
        public String ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private String _imgModuleId;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("imgModuleId", System.Data.DbType.String)]
        public String imgModuleId
        {
            get { return _imgModuleId; }
            set { _imgModuleId = value; }
        }
        private string _imgType;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("imgType", System.Data.DbType.String)]
        public string imgType
        {
            get { return _imgType; }
            set { _imgType = value; }
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
