using System;
using System.Runtime.Serialization;

namespace YHPT.SystemInfo.Model.BfRegion
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BF_REGION", "BfRegion", new string[] { "ID" })]
    [KnownType(typeof(BfRegionInfo))]
    public class BfRegionInfo : MB.Orm.Common.BaseModel
    {

        public BfRegionInfo()
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
        private string _CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CODE", System.Data.DbType.String)]
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NAME", System.Data.DbType.String)]
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private int? _PARENT_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("PARENT_ID", System.Data.DbType.Int32)]
        public int? PARENT_ID
        {
            get { return _PARENT_ID; }
            set { _PARENT_ID = value; }
        }
        private string _REGION_LEVEL;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REGION_LEVEL", System.Data.DbType.String)]
        public string REGION_LEVEL
        {
            get { return _REGION_LEVEL; }
            set { _REGION_LEVEL = value; }
        }
        private string _REMARK;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("REMARK", System.Data.DbType.String)]
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        private DateTime? _LAST_MODIFIED_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_DATE", System.Data.DbType.DateTime)]
        public DateTime? LAST_MODIFIED_DATE
        {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }
        private int? _SQU_NUM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SQU_NUM", System.Data.DbType.Int32)]
        public int? SQU_NUM
        {
            get { return _SQU_NUM; }
            set { _SQU_NUM = value; }
        }
    }

}
