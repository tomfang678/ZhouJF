using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfRoleUser;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.Management.WebUI.Controllers
{
    public class BfUserController : BaseController
    {
        private const string DEFAULT_PASSWORD = "111111";
        //
        // GET: /BfUser/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryBfUser(JQueryDataTablesModel jQueryDataTablesModel, QueryBfUserDto query)
        {
            var queryParam = new QueryBfUserDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                CODE = query.CODE,
                NAME = query.NAME,
                OWNER_ID = query.OWNER_ID,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new BfUserManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

        [MenuItem("~/BfUser/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new BfUserManager()).GetItemByKey(id);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/BfUser/Index", AuthorizeKey.Update)]
        public JsonResult Edit(BfUserInfo model, string gridStage)
        {
            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfUserManager()).Update(model);
            if (result)
            {
                List<BfRoleUserInfo> newRoleUserInfos = null;
                if (Request["hid_roles"] != null)
                {
                    newRoleUserInfos = new List<BfRoleUserInfo>();
                    foreach (var role in Request["hid_roles"].Split(','))
                    {
                        if (!String.IsNullOrEmpty(role))
                        {
                            newRoleUserInfos.Add(new BfRoleUserInfo()
                            {
                                BF_ROLE_ID = Convert.ToInt32(role),
                                BF_USER_ID = model.ID,
                                LAST_MODIFIED_DATE = DateTime.Now,
                                LAST_MODIFIED_USER = UserSession.Current.UserID
                            });
                        }
                    }
                }
                var oldRoleUserList = (new BfRoleUserManager()).GetItems(new BfRoleUserInfo()
                {
                    BF_USER_ID = model.ID
                });
                (new BfRoleUserManager()).UpdateRoleUser(oldRoleUserList, newRoleUserInfos);

                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
        }

        [HttpPost]
        [MenuItem("~/BfUser/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new BfUserManager()).Delete(id);
            if (result)
            {
                (new BfRoleUserManager()).DeleteByUserId(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [MenuItem("~/BfUser/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [MenuItem("~/BfUser/Index", AuthorizeKey.Add)]
        public JsonResult Add(BfUserInfo model)
        {
            if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.NAME))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            model.LAST_CHANGPWD_DATE = DateTime.Now;
            model.PASSWORD = MB.Util.DESDataEncrypt.EncryptString(DEFAULT_PASSWORD);

            var result = (new BfUserManager()).Add(model);
            if (result > 0)
            {
                List<BfRoleUserInfo> roleUserInfos = null;
                if (Request["hid_roles"] != null)
                {
                    roleUserInfos = new List<BfRoleUserInfo>();
                    foreach (var role in Request["hid_roles"].Split(','))
                    {
                        if (!String.IsNullOrEmpty(role))
                        {
                            roleUserInfos.Add(new BfRoleUserInfo()
                            {
                                BF_ROLE_ID = Convert.ToInt32(role),
                                BF_USER_ID = result,
                                LAST_MODIFIED_DATE = DateTime.Now,
                                LAST_MODIFIED_USER = UserSession.Current.UserID
                            });
                        }
                    }
                }
                //批量插入绑定该角色
                if (roleUserInfos != null && roleUserInfos.Count > 0)
                    (new BfRoleUserManager()).BatchAdd(roleUserInfos);
                return Json(new ResponseMessage() { IsSuccess = true });
            }

            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
        }

        [MenuItem("~/BfUser/Index", AuthorizeKey.Add)]
        public ActionResult ChangePwd(int id)
        {
            var entity = (new BfUserManager()).GetItemByKey(id);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/BfUser/Index", AuthorizeKey.Add)]
        public JsonResult ChangePwd(int id, string OLDPASSWORD, string NEWPASSWORD)
        {
            if (id < 1)
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "数据错误" });
            }
            if (string.IsNullOrEmpty(OLDPASSWORD) || string.IsNullOrEmpty(NEWPASSWORD))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            var entity = (new BfUserManager()).GetItemByKey(id);
            var tmpOldPwd = MB.Util.DESDataEncrypt.EncryptString(OLDPASSWORD);
            if (!entity.PASSWORD.Equals(tmpOldPwd))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "原密码不正确" });
            }
            entity.PASSWORD = MB.Util.DESDataEncrypt.EncryptString(NEWPASSWORD);
            entity.LAST_MODIFIED_USER = UserSession.Current.UserID;
            entity.LAST_CHANGPWD_DATE = DateTime.Now;
            entity.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfUserManager()).ChangePwd(entity);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改密码失败" });
        }
    }
}
