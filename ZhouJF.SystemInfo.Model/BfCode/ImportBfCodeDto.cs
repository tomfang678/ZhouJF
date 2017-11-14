using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YHPT.SystemInfo.Model.BfCode
{
    [DataContract]
    public class ImportBfCodeDto : BfCodeInfo
    {
        private string _CODE;
        [DataMember]
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        private string _DESCRIPTION;
        [DataMember]
        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
        private string _DETAILCODE;
        [DataMember]
        public string DETAILCODE
        {
            get { return _DETAILCODE; }
            set { _DETAILCODE = value; }
        }
        private string _DETAILNAME;
        [DataMember]
        public string DETAILNAME
        {
            get { return _DETAILNAME; }
            set { _DETAILNAME = value; }
        }
        private string _SEQ_NUM;
        [DataMember]
        public string SEQ_NUM
        {
            get { return _SEQ_NUM; }
            set { _SEQ_NUM = value; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]        
        public string ErrorMsg { get; set; }
    }
}
