using MB.Framework.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.Management.WebUI.Controllers
{
    public class CommonController : BaseController
    {
        public void GetDropDownList(int roadId = 1)
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", roadId);

            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", roadId);

            var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());
            ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", roadId);
        }
        //public SelectList GetDropDownList(int roadId)
        //{
        //    var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
        //    return new SelectList(roadList, "ID", "RoadName", roadId);
        //}
        //public ActionResult Query(JQueryDataTablesModel jQueryDataTablesModel, QueryDto query)
        //{
        //    var queryParam = new QueryBfUserDto
        //    {
        //        PageIndex = jQueryDataTablesModel.PageIndex,
        //        PageSize = jQueryDataTablesModel.PageSize,
        //        CODE = query.CODE,
        //        NAME = query.NAME,
        //        OWNER_ID = query.OWNER_ID,
        //        SortField = jQueryDataTablesModel.SortField,
        //        SortDirection = jQueryDataTablesModel.Direction
        //    };
        //    var pageList = (new BfUserManager()).GetPagedList(queryParam);
        //    return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        //}

        //[MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Update)]
        //public ActionResult Edit(int id)
        //{
        //    var entity = (new BfUserManager()).GetItemByKey(id);
        //    return View(entity);
        //}

        //[HttpPost]
        //[MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Update)]
        //public JsonResult Edit(BfUserInfo model, string gridStage)
        //{
        //    model.LAST_MODIFIED_USER = UserSession.Current.UserID;
        //    model.LAST_MODIFIED_DATE = DateTime.Now;
        //    var result = (new BfUserManager()).Update(model);
        //    if (result)
        //    {
        //        List<BfRoleUserInfo> newRoleUserInfos = null;
        //        if (Request["hid_roles"] != null)
        //        {
        //            newRoleUserInfos = new List<BfRoleUserInfo>();
        //            foreach (var role in Request["hid_roles"].Split(','))
        //            {
        //                if (!String.IsNullOrEmpty(role))
        //                {
        //                    newRoleUserInfos.Add(new BfRoleUserInfo()
        //                    {
        //                        BF_ROLE_ID = Convert.ToInt32(role),
        //                        BF_USER_ID = model.ID,
        //                        LAST_MODIFIED_DATE = DateTime.Now,
        //                        LAST_MODIFIED_USER = UserSession.Current.UserID
        //                    });
        //                }
        //            }
        //        }
        //        var oldRoleUserList = (new BfRoleUserManager()).GetItems(new BfRoleUserInfo()
        //        {
        //            BF_USER_ID = model.ID
        //        });
        //        (new BfRoleUserManager()).UpdateRoleUser(oldRoleUserList, newRoleUserInfos);

        //        return Json(new ResponseMessage() { IsSuccess = true });
        //    }
        //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
        //}

        //[HttpPost]
        //[MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Delete)]
        //public JsonResult Delete(int id)
        //{
        //    var result = (new BfUserManager()).Delete(id);
        //    if (result)
        //    {
        //        (new BfRoleUserManager()).DeleteByUserId(id);
        //        return Json(new ResponseMessage() { IsSuccess = true });
        //    }
        //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        //}

        //[MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Add)]
        //public ActionResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Add)]
        //public JsonResult Add(BfUserInfo model)
        //{
        //    if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.NAME))
        //    {
        //        return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
        //    }
        //    model.LAST_MODIFIED_USER = UserSession.Current.UserID;
        //    model.LAST_MODIFIED_DATE = DateTime.Now;
        //    model.LAST_CHANGPWD_DATE = DateTime.Now;
        //    model.PASSWORD = MB.Util.DESDataEncrypt.EncryptString(DEFAULT_PASSWORD);

        //    var result = (new BfUserManager()).Add(model);
        //    if (result > 0)
        //    {
        //        List<BfRoleUserInfo> roleUserInfos = null;
        //        if (Request["hid_roles"] != null)
        //        {
        //            roleUserInfos = new List<BfRoleUserInfo>();
        //            foreach (var role in Request["hid_roles"].Split(','))
        //            {
        //                if (!String.IsNullOrEmpty(role))
        //                {
        //                    roleUserInfos.Add(new BfRoleUserInfo()
        //                    {
        //                        BF_ROLE_ID = Convert.ToInt32(role),
        //                        BF_USER_ID = result,
        //                        LAST_MODIFIED_DATE = DateTime.Now,
        //                        LAST_MODIFIED_USER = UserSession.Current.UserID
        //                    });
        //                }
        //            }
        //        }
        //        //批量插入绑定该角色
        //        if (roleUserInfos != null && roleUserInfos.Count > 0)
        //            (new BfRoleUserManager()).BatchAdd(roleUserInfos);
        //        return Json(new ResponseMessage() { IsSuccess = true });
        //    }

        //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
        //}


    }
}
