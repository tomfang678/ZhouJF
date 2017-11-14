using System.Security.Principal;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     用户对象的基本功能
    /// </summary>
    public class HttpPrincipal : IPrincipal
    {
        private IIdentity identity;

        /// <summary>
        ///     获取当前用户的标识。
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                if (identity == null)
                {
                    identity = new HttpIdentity();
                }
                return identity;
            }
        }

        /// <summary>
        ///     确定当前用户是否属于指定的角色。
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return false;
        }
    }
}