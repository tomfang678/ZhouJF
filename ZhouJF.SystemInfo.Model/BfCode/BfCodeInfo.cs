using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MB.Orm.Common;

namespace YHPT.SystemInfo.Model.BfCode
{
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BF_CODE", "BfCode", new string[] { "ID" })]
    [KnownType(typeof(BfCodeInfo))]
    public class BfCodeInfo : BaseModel
    {

        public BfCodeInfo()
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
        private string _CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CODE", System.Data.DbType.String)]
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _DESCRIPTION;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("DESCRIPTION", System.Data.DbType.String)]
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        private DateTime _LAST_MODIFIED_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_DATE", System.Data.DbType.DateTime)]
        public DateTime LAST_MODIFIED_DATE
        {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }

        /// <summary>
        /// 明细
        /// </summary>
        [DataMember]
        public List<BfCodeDetailInfo> BfCodeDtlInfoList { get; set; }
    } 
}
