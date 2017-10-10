using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.CommonLogic
{
    public class CheckEntityResult
    {
        public bool Success { get; set; }
        public bool NeedCallBack { get; set; }
        public string Msg { get; set; }
        public string ErrorKey { get; set; }
        public CheckEntityResult(string msg, bool success = true, bool needCallBack = false,string errorKey = null)
        {
            this.Success = success;
            this.NeedCallBack = needCallBack;
            this.Msg = msg;
            this.ErrorKey = errorKey;
        }
    }
}