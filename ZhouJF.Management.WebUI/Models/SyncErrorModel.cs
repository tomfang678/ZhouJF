using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.Models
{
    /// <summary>
    /// 商品款同步异常
    /// </summary>
    public class SyncErrorModel
    {
        /// <summary>
        /// 6位编码
        /// </summary>
        public string ProductClsCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}