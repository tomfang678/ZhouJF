using System;
using System.Runtime.Serialization;

namespace YHPT.SystemInfo.Model.BfCode
{
    // 需要引用的命名空间  using System.Runtime.Serialization;  test

    /// <summary> 
    /// 文件生成时间 2009-12-18 11:09 
    /// </summary> 
    [DataContract]
    [MB.Orm.Mapping.Att.ModelMap("BF_CODE_DETAIL", "BfCodeDetail", new string[] { "ID" })]
    public class BfCodeDetailInfo: MB.Orm.Common.BaseModel {

        public BfCodeDetailInfo() {

        }
        private int _ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("ID", System.Data.DbType.Int32)]
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _BF_CODE_ID;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("BF_CODE_ID", System.Data.DbType.Int32)]
        public int BF_CODE_ID {
            get { return _BF_CODE_ID; }
            set { _BF_CODE_ID = value; }
        }
        private string _CODE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("CODE", System.Data.DbType.String)]
        public string CODE {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _NAME;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("NAME", System.Data.DbType.String)]
        public string NAME {
            get { return _NAME; }
            set { _NAME = value; }
        }
        private string _SEQ_NUM;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("SEQ_NUM", System.Data.DbType.String)]
        public string SEQ_NUM {
            get { return _SEQ_NUM; }
            set { _SEQ_NUM = value; }
		}

        private DateTime _LAST_MODIFIED_DATE;
        [DataMember]
        [MB.Orm.Mapping.Att.ColumnMap("LAST_MODIFIED_DATE", System.Data.DbType.DateTime)]
        public DateTime LAST_MODIFIED_DATE
        {
            get { return _LAST_MODIFIED_DATE; }
            set { _LAST_MODIFIED_DATE = value; }
        }

	} 



}
