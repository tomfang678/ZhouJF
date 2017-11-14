using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MB.Framework.Common;
using MB.Framework.Common.Model;
using YHPT.Management.WebUI.Library;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.Management.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly string _persistentCookie = CommonHelper.GetAppValue("PersistentCookie");
        /// <summary>
        /// 用户登录页面
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public ActionResult Sigin(string redirectUrl = null)
        {
            LoginPageStage stage = null;
            if (Request.Cookies.AllKeys.Contains(_persistentCookie) && Request.Cookies[_persistentCookie].Values.Count > 0)
            {
                stage = new LoginPageStage
                {
                    IsRemberLoginName = Request.Cookies[_persistentCookie].Values["rember"] == "1",
                    UserName = Request.Cookies[_persistentCookie].Values["user"] ?? string.Empty
                };
            }
            else
            {
                stage = new LoginPageStage
                {
                    IsRemberLoginName = false,
                    UserName = string.Empty
                };
            }

            return View(stage);
        }

        /// <summary>
        /// 用户登陆处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Sigin(ValidateUserDto data)
        {
            var bfUser = new BfUserManager();
            LoginPageStage stage = new LoginPageStage
            {
                IsRemberLoginName = data.RemberUserName,
                UserName = data.UserName
            };

            string enPswd = MB.Util.DESDataEncrypt.EncryptString(data.Password);
            var loginValidate = bfUser.GetSysLoginUserData(data.UserName, enPswd);
            if (loginValidate == null)
                ViewBag.Message = "网络错误，请联系管理员！";
            else
            {
                switch (loginValidate.Status)
                {
                    case UserValidateStatus.UserNotExist:
                        ViewBag.Message = "账号不存在";
                        break;
                    case UserValidateStatus.PasswordError:
                        ViewBag.Message = "密码错误";
                        break;
                    default:
                        {
                            LoginSucceed(loginValidate.Result, data.RemberUserName);
                            if (!string.IsNullOrEmpty(data.RedirectUrl))
                            {
                                return Redirect(data.RedirectUrl);
                            }
                            else
                            {
                                return Redirect(FormsAuthenticationService.DefaultUrl);
                            }
                        }
                }
            }
            return View(stage);
        }

        private void LoginSucceed(LoginUserInfo loginUser, bool isRemberUserName)
        {
            ViewBag.Message = null;
            FormsAuthenticationService.SignIn(loginUser, false);

            Session["LoginTime"] = DateTime.Now;

            if (isRemberUserName)
            {
                HttpCookie aCookie = new HttpCookie(_persistentCookie);
                aCookie.Values["user"] = loginUser.CODE;
                aCookie.Values["rember"] = "1";
                aCookie.Expires = DateTime.Now.AddDays(15);
                aCookie.Path = "/";
                Response.Cookies.Add(aCookie);
            }
            else
            {
                HttpCookie aCookie = new HttpCookie(_persistentCookie);
                aCookie.Value = string.Empty;
                aCookie.Expires = new DateTime(1900, 1, 1);
                aCookie.Path = "/";
                Response.Cookies.Add(aCookie);
            }
        }

        /// <summary>
        /// 用户注销登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthenticationService.SignOut();
            Session.Clear();
            Session.Abandon();
            return Redirect(FormsAuthenticationService.LoginUrl);
        }
    }

    public class LoginPageStage
    {
        public bool IsRemberLoginName { get; set; }

        public string UserName { get; set; }
    }
}
