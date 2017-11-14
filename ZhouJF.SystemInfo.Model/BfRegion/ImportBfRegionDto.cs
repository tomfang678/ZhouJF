using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YHPT.SystemInfo.Model.BfRegion
{
    [DataContract]
    public class ImportBfRegionDto : BfRegionInfo
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string ErrorMsg { get; set; }
    }
}
