using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using MB.Framework.Common.Model;
using MB.Framework.Common.Utility;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     表单认证服务帮助类
    /// </summary>
    public class FormsAuthenticationService
    {
        /// <summary>
        ///     登出页地址
        /// </summary>
        public static string LogoutUrl = "~/Account/Logout";

        /// <summary>
        ///     Cookie加密的后缀
        /// </summary>
        private static readonly byte[] appKey;

        static FormsAuthenticationService()
        {
            var _Config = WebConfigurationManager.OpenWebConfiguration("~");
            var system = (SystemWebSectionGroup) _Config.GetSectionGroup("system.web");
            IsEnabled = system.Authentication.Mode == AuthenticationMode.Forms;
            if (!IsEnabled)
            {
                return;
            }

            if (!_Config.AppSettings.Settings.AllKeys.Contains("AuthKey"))
            {
                appKey = Guid.NewGuid().ToByteArray();
                _Config.AppSettings.Settings.Add("AuthKey", Convert.ToBase64String(appKey));
                _Config.Save();
            }
            else
            {
                appKey = Convert.FromBase64String(_Config.AppSettings.Settings["AuthKey"].Value);
            }

            CookieDomain = system.Authentication.Forms.Domain;
            CookieName = system.Authentication.Forms.Name + "Token"; //alert:无法直接使用system.Authentication.Forms.Name
            CookiePath = system.Authentication.Forms.Path;
            LoginUrl = system.Authentication.Forms.LoginUrl;
            DefaultUrl = system.Authentication.Forms.DefaultUrl;
            Timeout = system.Authentication.Forms.Timeout;
        }

        /// <summary>
        ///     登陆页地址
        /// </summary>
        public static string LoginUrl { get; private set; }

        /// <summary>
        ///     Cookie生效的域名
        /// </summary>
        public static string CookieDomain { get; private set; }

        /// <summary>
        ///     认证的Cookie的名称
        /// </summary>
        public static string CookieName { get; private set; }

        /// <summary>
        ///     CookiePath
        /// </summary>
        public static string CookiePath { get; private set; }

        /// <summary>
        ///     是否启用
        /// </summary>
        public static bool IsEnabled { get; private set; }

        /// <summary>
        ///     Cookie过期时长
        /// </summary>
        public static TimeSpan Timeout { get; private set; }

        /// <summary>
        ///     网站的默认页
        /// </summary>
        public static string DefaultUrl { get; private set; }

        /// <summary>
        ///     执行登陆操作，向客服端输出认证Cookie
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="accessToken"></param>
        /// <param name="createPersistentCookie"></param>
        public static void SignIn(LoginUserInfo loginUser, bool createPersistentCookie)
        {
            var cookie = new HttpCookie(CookieName);
            cookie.Value = CreateAuthToken(loginUser, Timeout);
            cookie.Domain = CookieDomain;
            cookie.Expires = DateTime.Now.Add(Timeout);
            cookie.Path = "/";
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        ///     登出，使认证Cookie过期
        /// </summary>
        public static void SignOut()
        {
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(CookieName))
            {
                var cookie = HttpContext.Current.Request.Cookies[CookieName];
                cookie.Expires = new DateTime(1900, 01, 01);
                cookie.Path = "/";
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }

        /// <summary>
        ///     验证认证Cookie是否存在并有效
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool VerifyAuthToken(out LoginUserInfo loginUser)
        {
            loginUser = new LoginUserInfo();

            byte[] data;
            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains(CookieName))
            {
                return false;
            }
            try
            {
                var cookieValue = HttpContext.Current.Request.Cookies[CookieName].Value;
                data = HttpServerUtility.UrlTokenDecode(cookieValue);
                var formatter = new BinaryFormatter();
                var stream = new MemoryStream(data);
                var token = (CookieTokenInfo) formatter.Deserialize(stream);
                stream.Dispose();

                if (token.Signature.Length != 20)
                {
                    return false;
                }
                if (token.ExpiredDate < DateTime.Now)
                {
                    return false;
                }
                if (token.UserName == null)
                {
                    return false;
                }
                if (token.AccessToken == null)
                {
                    return false;
                }
                if (token.UID < 1)
                {
                    return false;
                }

                data =
                    Encoding.UTF8.GetBytes(token.UserName)
                        .Concat(Encoding.UTF8.GetBytes(token.UID.ToString()))
                        .Concat(Encoding.UTF8.GetBytes(token.AccessToken))
                        .Concat(BitConverter.GetBytes(token.ExpiredDate.ToBinary()))
                        .Concat(appKey)
                        .ToArray();

                using (var sha1 = SHA1.Create())
                {
                    data = sha1.ComputeHash(data);
                }

                for (var i = 0; i < 20; i++)
                {
                    if (data[i] != token.Signature[i])
                    {
                        return false;
                    }
                }
                loginUser.CODE = token.UserName;
                loginUser.ACCESSTOKEN = token.AccessToken;
                loginUser.ID = token.UID;

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     创建人认证Cookie
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="expireSpan"></param>
        /// <returns></returns>
        public static string CreateAuthToken(LoginUserInfo loginUser, TimeSpan expireSpan)
        {
            var token = new CookieTokenInfo();
            token.UserName = loginUser.CODE;
            token.AccessToken = loginUser.ACCESSTOKEN;
            token.UID = loginUser.ID;
            token.ExpiredDate = DateTime.Now.Add(expireSpan);

            var data =
                Encoding.UTF8.GetBytes(token.UserName)
                    .Concat(Encoding.UTF8.GetBytes(token.UID.ToString()))
                    .Concat(Encoding.UTF8.GetBytes(token.AccessToken))
                    .Concat(BitConverter.GetBytes(token.ExpiredDate.ToBinary()))
                    .Concat(appKey)
                    .ToArray();
            using (var sha1 = SHA1.Create())
            {
                token.Signature = sha1.ComputeHash(data);
            }
            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();
            formatter.Serialize(stream, token);
            data = stream.ToArray();
            stream.Dispose();
            return HttpServerUtility.UrlTokenEncode(data);
        }

        /// <summary>
        ///     用户信息加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            return MD5Handler.EnMD5(text);
        }
    }
}