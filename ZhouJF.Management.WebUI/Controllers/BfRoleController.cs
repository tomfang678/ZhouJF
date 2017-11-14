using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfModule;
using YHPT.SystemInfo.Model.BfRole;
using YHPT.SystemInfo.Model.BfRoleUser;

namespace YHPT.Management.WebUI.Controllers
{
    public class BfRoleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryBfRole(JQueryDataTablesModel jQueryDataTablesModel, QueryBfRoleDto query)
        {
            var queryParam = new QueryBfRoleDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                CODE = query.CODE,
                NAME = query.NAME,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new BfRoleManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

        [MenuItem("~/BfRole/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new BfRoleManager()).GetItemByKey(id);
            return View(entity);
        }

        [System.Web.Mvc.HttpPost]
        [MenuItem("~/BfRole/Index", AuthorizeKey.Update)]
        public JsonResult Edit(BfRoleInfo model, string gridStage)
        {
            if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.NAME))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }

            #region 通用权限
            List<BfRolePermissionInfo> rolePermissionInfos = null;
            if (Request["hid_operations"] != null)
            {
                rolePermissionInfos = new List<BfRolePermissionInfo>();
                foreach (var operation in Request["hid_operations"].Split(','))
                {
                    if (!String.IsNullOrEmpty(operation))
                    {
                        rolePermissionInfos.Add(new BfRolePermissionInfo()
                        {
                            BF_MODULE_OPERATION_ID = Convert.ToInt32(operation),
                            BF_ROLE_ID = model.ID
                        });
                    }
                }
            }
            model.OperationItems = rolePermissionInfos;
            #endregion

            #region 特殊权限
            List<BfRoleSpecialPermissionInfo> otherOperationInfos = null;
            if (Request["hid_otherOperations"] != null)
            {
                otherOperationInfos = new List<BfRoleSpecialPermissionInfo>();
                foreach (var otherOperation in Request["hid_otherOperations"].Split(','))
                {
                    if (!String.IsNullOrEmpty(otherOperation))
                    {
                        otherOperationInfos.Add(new BfRoleSpecialPermissionInfo()
                        {
                            BF_MODULE_SPECIAL_ID = Convert.ToInt32(otherOperation),
                            BF_ROLE_ID = model.ID
                        });
                    }
                }
            }
            model.OtherOperationItems = otherOperationInfos;
            #endregion

            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfRoleManager()).Update(model);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
        }

        [System.Web.Mvc.HttpPost]
        [MenuItem("~/BfRole/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new BfRoleManager()).Delete(id);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [MenuItem("~/BfRole/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [MenuItem("~/BfRole/Index", AuthorizeKey.Add)]
        public JsonResult Add(BfRoleInfo model)
        {
            if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.NAME))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            #region 通用权限
            List<BfRolePermissionInfo> rolePermissionInfos = null;
            if (Request["hid_operations"] != null)
            {
                rolePermissionInfos = new List<BfRolePermissionInfo>();
                foreach (var operation in Request["hid_operations"].Split(','))
                {
                    if (!String.IsNullOrEmpty(operation))
                    {
                        rolePermissionInfos.Add(new BfRolePermissionInfo()
                        {
                            BF_MODULE_OPERATION_ID = Convert.ToInt32(operation)
                        });
                    }
                }
            }
            model.OperationItems = rolePermissionInfos;
            #endregion

            #region 特殊权限
            List<BfRoleSpecialPermissionInfo> otherOperationInfos = null;
            if (Request["hid_otherOperations"] != null)
            {
                otherOperationInfos = new List<BfRoleSpecialPermissionInfo>();
                foreach (var otherOperation in Request["hid_otherOperations"].Split(','))
                {
                    if (!String.IsNullOrEmpty(otherOperation))
                    {
                        otherOperationInfos.Add(new BfRoleSpecialPermissionInfo()
                        {
                            BF_MODULE_SPECIAL_ID = Convert.ToInt32(otherOperation)
                        });
                    }
                }
            }
            model.OtherOperationItems = otherOperationInfos;
            #endregion

            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfRoleManager()).Add(model);

            if (result > 0)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
        }

        public JsonResult GetAllRole()
        {
            var data = (new BfRoleManager()).GetItems(new QueryBfRoleDto());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllRoleByUserId(int UserId)
        {
            List<CurrentUserRoleInfo> currentUserRoleInfoList = new List<CurrentUserRoleInfo>();
            var data = (new BfRoleManager()).GetItems(new QueryBfRoleDto());

            if (data != null)
            {
                foreach (var role in data)
                {
                    var roleUserList = (new BfRoleUserManager()).GetItems(new BfRoleUserInfo()
                    {
                        BF_USER_ID = UserId
                    });
                    currentUserRoleInfoList.Add(new CurrentUserRoleInfo()
                    {
                        CODE = role.CODE,
                        NAME = role.NAME,
                        IS_ADMIN = role.IS_ADMIN,
                        ID = role.ID,
                        IsSelect = roleUserList.Exists(a => a.BF_ROLE_ID == role.ID)
                    });
                }
            }
            return Json(currentUserRoleInfoList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllOperation()
        {
            var allOperationList = (new BfRoleManager()).SelectAllOperation();
            return Json(allOperationList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllOperationByUserId(int roleId)
        {
            List<CurrentBfModuleInfo> currentBfModuleInfos = new List<CurrentBfModuleInfo>();
            List<BfRolePermissionInfo> rolePermissionInfos = (new BfRolePermissionManager()).GetItems(new QueryBfRolePermissionDto()
                    {
                        BF_ROLE_ID = roleId
                    });
            List<BfRoleSpecialPermissionInfo> roleSpecialPermissionInfos =
                (new BfRoleSpecialPermissionManager()).GetItems(new QueryBfRoleSpecialPermissionDto()
                {
                    BF_ROLE_ID = roleId
                });

            var allOperationList = (new BfRoleManager()).SelectAllOperation();

            if (allOperationList != null)
            {
                foreach (var module in allOperationList)
                {
                    #region 有哪些通用权限
                    List<CurrentBfModuleOperationInfo> currentModuleOperations = new List<CurrentBfModuleOperationInfo>();
                    foreach (var operationItem in module.OperationItems)
                    {
                        currentModuleOperations.Add(new CurrentBfModuleOperationInfo()
                        {
                            BF_MODULE_ID = operationItem.BF_MODULE_ID,
                            ID = operationItem.ID,
                            CODE = operationItem.CODE,
                            IsSelect = rolePermissionInfos.Exists(a=>a.BF_MODULE_OPERATION_ID==operationItem.ID)
                        });
                    }
                    #endregion

                    #region 有哪些特殊权限
                    List<CurrentBfModuleOtherOperationInfo> currentBfModuleOtherOperationInfos = new List<CurrentBfModuleOtherOperationInfo>();
                    foreach (var otherOperation in module.OtherOperationItems)
                    {
                        currentBfModuleOtherOperationInfos.Add(new CurrentBfModuleOtherOperationInfo()
                        {
                            BF_MODULE_ID = otherOperation.BF_MODULE_ID,
                            ID = otherOperation.ID,
                            CODE = otherOperation.CODE,
                            NAME = otherOperation.NAME,
                            IsSelect = roleSpecialPermissionInfos.Exists(a => a.BF_MODULE_SPECIAL_ID == otherOperation.ID)
                        });
                    }
                    #endregion

                    currentBfModuleInfos.Add(new CurrentBfModuleInfo()
                    {
                        ID = module.ID,
                        CODE = module.CODE,
                        NAME = module.NAME,
                        CurrentBfModuleOperationInfos = currentModuleOperations,
                        CurrentBfModuleOtherOperationInfos = currentBfModuleOtherOperationInfos
                    });
                }
            }
            return Json(currentBfModuleInfos, JsonRequestBehavior.AllowGet);
        }
    }

    public class CurrentUserRoleInfo : BfRoleInfo
    {

        public bool IsSelect { get; set; }
    }

    public class CurrentBfModuleOperationInfo : BfModuleOperationInfo
    {
        public bool IsSelect { get; set; }
    }

    public class CurrentBfModuleOtherOperationInfo : BfModuleOtherOperationInfo
    {
        public bool IsSelect { get; set; }
    }

    public class CurrentBfModuleInfo : BfModuleInfo
    {
        public List<CurrentBfModuleOperationInfo> CurrentBfModuleOperationInfos { get; set; }
        public List<CurrentBfModuleOtherOperationInfo> CurrentBfModuleOtherOperationInfos { get; set; }
    }
}


