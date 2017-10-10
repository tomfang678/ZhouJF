using System;
using System.Web.Mvc;

namespace YHPT.Management.WebUI.Library
{
    public class MenuItemAttribute : ActionFilterAttribute
    {
        public MenuItemAttribute(string path, string authOper)
        {
            Path = path;
            AuthOper = authOper;
        }

        public MenuItemAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
        public string AuthOper { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Authorize(filterContext, Path))
                filterContext.Result = new ContentResult { Content = "<script>alert('抱歉,你不具有当前操作的权限！');history.go(-1)</script>" };

            if (!string.IsNullOrEmpty(AuthOper) && !UserSession.Current.HasPermit(Path, AuthOper))
            {
                filterContext.Result = new ContentResult { Content = "<script>alert('抱歉,你不具有当前操作的权限！');history.go(-1)</script>" };
            }

            GetModuleIdByPage(filterContext, Path);
        }

        /// <summary>
        ///     根据当前地址获取模块ID
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        private void GetModuleIdByPage(ActionExecutingContext filterContext, string permissionName)
        {
            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");
            //根据当前地址获取模块ID
            filterContext.HttpContext.Items["ModuleID"] = UserSession.Current.GetModuleIdByPage(permissionName);
        }

        protected virtual bool Authorize(ActionExecutingContext filterContext, string permissionName)
        {
            if (filterContext.HttpContext == null)
                throw new ArgumentNullException("httpContext");
            //验证是否登录
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                return false;
            //验证模块权限
            return UserSession.Current.CheckPagePermit(permissionName);
        }
    }
}