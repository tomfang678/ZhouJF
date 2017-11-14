using System.Security.Principal;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     标识对象的基本功能
    /// </summary>
    public class HttpIdentity : IIdentity
    {
        private bool? _IsAuthenticated;

        /// <summary>
        ///     当前认证类型
        /// </summary>
        public string AuthenticationType
        {
            get { return "YHPTAUTH"; }
        }

        /// <summary>
        ///     当前用户是否登录
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                //if (_IsAuthenticated.HasValue)
                //{
                //    return _IsAuthenticated.Value;
                //}
                //else
                //{
                LoginUserInfo userInfo;

                var tokenValid = FormsAuthenticationService.VerifyAuthToken(out userInfo);
                if (tokenValid && string.IsNullOrEmpty(userInfo.CODE) == false)
                {
                    if (UserSession.Current.IsValid)
                    {
                        _IsAuthenticated = true;
                    }
                    else
                    {
                        _IsAuthenticated = UserSession.Current.SyncUserData(userInfo);
                    }
                }
                else
                {
                    _IsAuthenticated = false;
                }
                //}

                return _IsAuthenticated.Value;
            }
        }

        /// <summary>
        ///     当前登陆的用户名
        /// </summary>
        public string Name
        {
            get { return UserSession.Current.UserCode; }
        }
    }
}