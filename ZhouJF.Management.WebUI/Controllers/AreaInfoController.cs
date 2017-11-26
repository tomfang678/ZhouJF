using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.CommonLogic;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhAreaInfo;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 区域信息
    /// </summary>
    public class AreaInfoController : BaseController
    {
        //
        // GET: /AreaInfo/

        public ActionResult Index()
        {
            ViewBag.region = new SelectList(CommonFeild.GetAreaList(), "Name", "Name", "浦东新区");
            return View();
        }
        [MenuItem("~/AreaInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            ViewBag.region = new SelectList(CommonFeild.GetAreaList(), "Name", "Name", "浦东新区");
            return View();
        }


        [MenuItem("~/AreaInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhAreainfoManager()).GetItemByKey(id);
            ViewBag.region = new SelectList(CommonFeild.GetAreaList(), "Name", "Name", entity.region);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/AreaInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(AreaInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID == null ? "" : UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhAreainfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/AreaInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhAreainfoManager()).Delete(id);
            if (result)
            {
                (new YhAreainfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/AreaInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(AreaInfo model)
        {
            if (string.IsNullOrEmpty(model.Owner) || string.IsNullOrEmpty(model.Owner))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhAreainfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, AreaInfoDto query)
        {
            var queryParam = new AreaInfoDto
            {
                ID = query.ID,
                AreaCode = query.AreaCode,
                Owner = query.Owner,
                Dept = query.Dept,
                Area = query.Area,
                Section = query.Section,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhAreainfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}
