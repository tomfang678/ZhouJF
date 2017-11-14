using System;
using System.Collections.Generic;
using System.Web;
using MB.Framework.Common.Enums;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfModule;
using YHPT.SystemInfo.Model.BfRole;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     用户会话信息
    /// </summary>
    [Serializable]
    public class UserSession
    {
        private const string USER_SESSION_KEY = "YHPTAUTH";
        private const int ANONYMOUS_USER_ID = 0;
        private const string ANONYMOUS_USER_CODE = "anonymous";
        private const string ANONYMOUS_USER_NAME = "未登录用户";

        private UserSession()
        {
            UserID = ANONYMOUS_USER_ID;
            UserCode = ANONYMOUS_USER_CODE;
            UserName = ANONYMOUS_USER_NAME;
            AccessToken = "";
            ModuleInfos = new List<ModuleInfo>();
            DisplayModuleInfo = null;
            RoleOperation = new List<ModuleOperationInfo>();
        }

        /// <summary>
        ///     返回当前会话的用户信息
        /// </summary>
        public static UserSession Current
        {
            get
            {
                UserSession value;
                if (HttpContext.Current.Session == null || HttpContext.Current.Session[USER_SESSION_KEY] == null)
                {
                    value = new UserSession();
                    if (HttpContext.Current.Session != null)
                    {
                        HttpContext.Current.Session[USER_SESSION_KEY] = value;
                    }
                }
                else
                {
                    value = HttpContext.Current.Session[USER_SESSION_KEY] as UserSession;
                }
                return value;
            }
        }

        /// <summary>
        ///     当前用户是不是已验证用户
        /// </summary>
        public bool IsValid
        {
            get { return UserID != ANONYMOUS_USER_ID; }
        }

        public bool IsAdmin
        {
            get
            {
                if (Roles != null)
                {
                    foreach (var bfRoleInfo in Roles)
                    {
                        if (bfRoleInfo.IS_ADMIN == 1)
                            return true;
                    }
                }
                return false;
                ////return true;
                //return UserCode == "SA";
            }
        }

        /// <summary>
        ///     当前用户ID
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        ///     当前用户名（登陆名）
        /// </summary>
        public string UserCode { get; private set; }

        /// <summary>
        ///     当前用户显示名称（昵称）
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        ///     调用服务端的TOKEN
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        ///     当前用户拥有的角色操作权限
        /// </summary>
        public List<ModuleOperationInfo> RoleOperation { get; set; }

        /// <summary>
        ///     当前用户拥有的角色操作权限
        /// </summary>
        public List<BfRoleInfo> Roles { get; set; }

        /// <summary>
        ///     用户图片地址
        /// </summary>
        public string Title_Url { get; set; }

        /// <summary>
        ///     用户配置过的模块权限
        /// </summary>
        public List<ModuleInfo> ModuleInfos { get; set; }

        public List<BfModuleInfo> DisplayModuleInfo { get; set; }

        /// <summary>
        ///     检查用户是否具有页面访问权限
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckPagePermit(string path)
        {
            if (DisplayModuleInfo == null)
                return false;
            foreach (var module in DisplayModuleInfo)
            {
                if (module.Items == null)
                    continue;
                if (module.Items.Exists(a => a.URL == path.Trim('~')))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     根据地址获取模块ID
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetModuleIdByPage(string path)
        {
            var moduleID = 0;
            if (ModuleInfos == null || DisplayModuleInfo == null)
                return moduleID;
            var entity = new BfModuleInfo();
            foreach (var module in DisplayModuleInfo)
            {
                if (module.Items == null)
                    continue;
                entity = module.Items.FindLast(a => a.URL == path.Trim('~'));
                if (entity != null && entity.ID > 0)
                    break;
            }

            if (entity != null && entity.ID > 0)
            {
                var info = ModuleInfos.FindLast(a => a.MODULE_CODE == entity.CODE);
                if (info != null && info.SystemID.GetValueOrDefault() > 0)
                    moduleID = info.SystemID.GetValueOrDefault();
            }
            return moduleID;
        }

        /// <summary>
        ///     获取页面有哪些权限
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public AuthorizeKey GetPagePermit(string path)
        {
            var authorizeKey = new AuthorizeKey();
            if (IsAdmin)
            {
                authorizeKey.CanAdd = true;
                authorizeKey.CanDelete = true;
                authorizeKey.CanUpdate = true;
                return authorizeKey;
            }
            //判断用户权限
            if (RoleOperation != null)
            {
                authorizeKey.CanAdd =
                    RoleOperation.FindAll(
                        a => !String.IsNullOrEmpty(a.MODULE_URL) && a.MODULE_URL.ToLower() == path.Trim('~').ToLower() && a.OPERATION_CODE == AuthorizeKey.Add)
                        .Count > 0;
                authorizeKey.CanDelete =
                    RoleOperation.FindAll(
                        a =>
                            !String.IsNullOrEmpty(a.MODULE_URL) &&
                            a.MODULE_URL.ToLower() == path.Trim('~').ToLower() &&
                            a.OPERATION_CODE == AuthorizeKey.Delete).Count > 0;
                authorizeKey.CanUpdate =
                    RoleOperation.FindAll(
                        a =>
                            !String.IsNullOrEmpty(a.MODULE_URL) &&
                            a.MODULE_URL.ToLower() == path.Trim('~').ToLower() &&
                            a.OPERATION_CODE == AuthorizeKey.Update).Count > 0;
                authorizeKey.CanOpen =
                    RoleOperation.FindAll(
                        a =>
                            !String.IsNullOrEmpty(a.MODULE_URL) &&
                            a.MODULE_URL.ToLower() == path.Trim('~').ToLower() &&
                            a.OPERATION_CODE == AuthorizeKey.Open).Count > 0;
            }
            return authorizeKey;
        }

        /// <summary>
        ///     当前地址是否有权限
        /// </summary>
        /// <param name="path"></param>
        /// <param name="authorizeKey"></param>
        /// <returns></returns>
        public bool HasPermit(string path, string authorizeKey)
        {
            if (IsAdmin)
                return true;
            if (RoleOperation != null)
            {
                return
                    RoleOperation.FindAll(
                        a =>
                            !String.IsNullOrEmpty(a.MODULE_URL) &&
                            a.MODULE_URL.ToLower() == path.Trim('~').ToLower() && a.OPERATION_CODE == authorizeKey)
                        .Count > 0;
            }
            return false;
        }

        /// <summary>
        ///     加载用户信息
        /// </summary>
        /// <param name="loginUserInfo"></param>
        public bool SyncUserData(LoginUserInfo loginUserInfo)
        {
            AccessToken = loginUserInfo.ACCESSTOKEN;
            UserID = loginUserInfo.ID;
            var userInfo = (new BfUserManager()).GetItemByCode(loginUserInfo.CODE);
            if (userInfo != null && userInfo.ID > 0)
            {
                UserID = userInfo.ID;
                UserCode = userInfo.CODE;
                UserName = userInfo.NAME;
                var modules = (new BfModuleManager()).SelectObjectByUserID(UserID);
                var moduleList = new List<ModuleInfo>();
                if (modules != null)
                {
                    foreach (var module in modules)
                    {
                        var mo = new ModuleInfo
                        {
                            SystemID = module.ID,
                            MODULE_CODE = module.CODE,
                            MODULE_NAME = module.NAME
                        };
                        moduleList.Add(mo);
                    }
                }
                ModuleInfos = moduleList;

                #region

                Roles = (new BfRoleUserManager()).GetRoleByUser(UserID);

                #endregion

                #region 操作权限

                var operationInfos = (new BfModuleOperationManager()).SelectOperationByUserID(UserID);
                var moduleOperationInfos = new List<ModuleOperationInfo>();
                if (operationInfos != null)
                {
                    foreach (var ro in operationInfos)
                    {
                        var role = new ModuleOperationInfo
                        {
                            MODULE_CODE = ro.MODULE_CODE,
                            OPERATION_CODE = ro.CODE
                        };
                        moduleOperationInfos.Add(role);
                    }
                }
                RoleOperation = moduleOperationInfos;
                MemuTree.Instance.GetModuleInfo(true);

                #endregion

                if (HttpContext.Current.Session["LoginTime"] == null)
                {
                    HttpContext.Current.Session["LoginTime"] = DateTime.Now;
                }
                return true;
            }
            return false;
        }
    }
}