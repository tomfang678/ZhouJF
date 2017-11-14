using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 养护基础数据管理
    /// </summary>
    public class YHBasicController : BaseController
    {
        //
        // GET: /YHBasic/

        public ActionResult Index()
        {
            return View();
        }

    }
}
