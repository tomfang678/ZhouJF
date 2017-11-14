using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;

namespace YHPT.Management.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (Session["LoginTime"] == null)
            {
                Session["LoginTime"] = DateTime.Now;
            }
            ViewBag.LoginTime = ((DateTime)Session["LoginTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }
    }
}
