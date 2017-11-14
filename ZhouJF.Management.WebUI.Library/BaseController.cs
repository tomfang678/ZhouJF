using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using MB.Framework.Common;
using MB.Framework.Common.Logging;
using YHPT.Management.WebUI.Library.Controls;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     自定义的Controller，实现页面访问认证
    /// </summary>
    public class BaseController : Controller
    {
        public static readonly string Request_Path_Item_Key = "RequestPath";
        public static readonly string Menu_Path_Item_Key = "MenuPath";
        private IPrincipal user;

        /// <summary>
        ///     为当前 HTTP 请求获取用户安全信息
        /// </summary>
        public new IPrincipal User
        {
            get
            {
                if (user == null)
                {
                    user = new HttpPrincipal();
                }

                return user;
            }
        }

        private NameValueCollection GetCurrentRequestParam()
        {
            string area = string.Empty, controller = string.Empty, action = string.Empty;
            if (ControllerContext.IsChildAction)
            {
                var parentContext = ControllerContext.ParentActionViewContext as ControllerContext;
                controller = parentContext.RequestContext.RouteData.Values["controller"].ToString().ToLower();
                action = parentContext.RequestContext.RouteData.Values["action"].ToString().ToLower();
                if (parentContext.RouteData.DataTokens["area"] != null)
                {
                    area = parentContext.RouteData.DataTokens["area"].ToString().ToLower();
                }
            }
            else
            {
                if (ControllerContext.RouteData.DataTokens["area"] != null)
                {
                    area = ControllerContext.RouteData.DataTokens["area"].ToString().ToLower();
                }

                controller = ControllerContext.RequestContext.RouteData.Values["controller"].ToString().ToLower();
                action = ControllerContext.RequestContext.RouteData.Values["action"].ToString().ToLower();
            }
            return new NameValueCollection {{"AREA", area}, {"CONTROLLER", controller}, {"ACTION", action}};
        }

        /// <summary>
        ///     在进行授权时调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var menuItemAttr = filterContext.ActionDescriptor.GetCustomAttributes(typeof (MenuItemAttribute), true);
            if (menuItemAttr.Count() > 0)
            {
                var menuItemPath = (menuItemAttr[0] as MenuItemAttribute).Path;
                if (!string.IsNullOrEmpty(menuItemPath))
                {
                    filterContext.HttpContext.Items[Menu_Path_Item_Key] = menuItemPath.Trim('~').ToLower();
                }
            }

            var requestParam = GetCurrentRequestParam();

            var requestPath =
                string.Join("/", requestParam["AREA"], requestParam["CONTROLLER"], requestParam["ACTION"]).ToLower();

            filterContext.HttpContext.Items[Request_Path_Item_Key] = requestPath;

            if (filterContext.IsChildAction)
            {
                base.OnAuthorization(filterContext);
                return;
            }

            HttpContext.User = User;


            if (FormsAuthenticationService.LoginUrl.Trim('~').ToLower().StartsWith(requestPath))
            {
                base.OnAuthorization(filterContext);
            }
            else if (HttpContext.Request.IsAjaxRequest())
            {
                base.OnAuthorization(filterContext);
            }
            else if (HttpContext.User.Identity.IsAuthenticated)
            {
                if ((HttpContext.Request).HttpMethod == "GET")
                {
                    LogHelper.Info(GetType(), UserSession.Current.UserCode + " visit:" + requestPath);
                }
                base.OnAuthorization(filterContext);
            }
            else
            {
                var loginUrl = string.Format("{0}?redirectUrl={1}", FormsAuthenticationService.LoginUrl,
                    HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
                filterContext.Result = new RedirectResult(loginUrl, true);
                //Response.Redirect(loginUrl, true);
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var requestParam = GetCurrentRequestParam();

            var requestPath =
                string.Join("/", requestParam["AREA"], requestParam["CONTROLLER"], requestParam["ACTION"]).ToLower();

            HttpContext.User = User;

            if (FormsAuthenticationService.LoginUrl.Trim('~').ToLower().StartsWith(requestPath))
            {
            }
            else if (HttpContext.Request.IsAjaxRequest())
            {
                base.OnActionExecuted(filterContext);
            }
            else if (HttpContext.User.Identity.IsAuthenticated)
            {
                base.OnActionExecuted(filterContext);
            }
            else
            {
                var loginUrl = string.Empty;
                if (FormsAuthenticationService.LogoutUrl.Trim('~').ToLower().StartsWith(requestPath))
                    loginUrl = string.Format("{0}", FormsAuthenticationService.LoginUrl);
                else
                    loginUrl = string.Format("{0}?redirectUrl={1}", FormsAuthenticationService.LoginUrl,
                        HttpUtility.UrlEncode(Request.Url.AbsoluteUri));
                filterContext.Result = new RedirectResult(loginUrl, true);
            }
        }

        /// <summary>
        ///     当操作中发生未经处理的异常时调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                var requestUrl = "";
                if (filterContext.RequestContext.HttpContext.Request.Url != null)
                    requestUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString();
                var exception =
                    new Exception(string.Format("请求URL:{0} | 异常说明: {1}\r\nStackTrace:{2}\r\n", requestUrl,
                        filterContext.Exception.Message, filterContext.Exception.StackTrace));
                //记录异常
                //filterContext.Result = View("Error", exception);
                //filterContext.ExceptionHandled = true;
                //filterContext.HttpContext.Response.Clear();
                //filterContext.HttpContext.Response.StatusCode = 500;
                //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                LogHelper.Error(GetType(), "Action请求异常:" + requestUrl, exception);
            }
            catch (Exception)
            {
            }
            base.OnException(filterContext);
        }

        /// <summary>
        ///     在调用操作方法前调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestParam = GetCurrentRequestParam();
            var requestPath =
                string.Join("/", requestParam["AREA"], requestParam["CONTROLLER"], requestParam["ACTION"]).ToLower();
            HttpContext.User = User;

            if (FormsAuthenticationService.LoginUrl.Trim('~').ToLower().StartsWith(requestPath))
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                #region 保证权限验证的界面是所有页面的Index页面
                var pagePermitUrl = string.Join("/", requestParam["AREA"], requestParam["CONTROLLER"], "index").ToLower();
                #endregion

                //设置授权属性，然后赋值给ViewBag保存
                ViewBag.AuthorizeKey = UserSession.Current.GetPagePermit(pagePermitUrl);
            }
        }

        #region Ajax Response

        public JsonResult AjaxResult<T>(T response)
        {
            return Json(new {success = true, data = response}, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        ///     确保文件夹在服务器上存在 不存在则创建
        /// </summary>
        /// <param name="folderName"></param>
        public void EnsureFolderOnserver(string folderName)
        {
            var path = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) + folderName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #region Grid Data Response

        /// <summary>
        ///     返回一个Jquery DataTable的结果数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="totalCount"></param>
        /// <param name="gridKey"></param>
        /// <returns></returns>
        public JsonResult DataTablesJson<T>(IEnumerable<T> data, int totalCount, int gridKey, object tagData = null)
        {
            var result = new JsonResult();
            result.Data = new JQueryDataTablesResponse<T>(data, totalCount, totalCount, gridKey, tagData);
            return result;
        }

        /// <summary>
        ///     返回一个Jquery DataTable的结果数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="gridKey"></param>
        /// <returns></returns>
        public JsonResult DataTablesJson<T>(PagedList<T> response, int gridKey, object tagData = null)
        {
            var result = new JsonResult();
            if (response == null)
            {
                result.Data = new JQueryDataTablesResponse<T>(Activator.CreateInstance<List<T>>(), 0, 0,
                    gridKey, tagData);
            }
            else
            {
                result.Data = new JQueryDataTablesResponse<T>(response.Data, response.TotalCount, response.TotalCount,
                    gridKey, tagData);
            }
            return result;
        }

        #endregion
    }
}