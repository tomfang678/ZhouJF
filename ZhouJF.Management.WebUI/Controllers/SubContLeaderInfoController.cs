using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.Management.WebUI.Controllers
{
    public class SubContLeaderInfoController : BaseController
    {
        //
        // GET: /SubContLeaderInfo/
        public ActionResult Index()
        {
            var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());
            ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", 1);

            return View();
        }

        [MenuItem("~/SubContLeaderInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());
            ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", 1);

            return View();
        }


        [MenuItem("~/SubContLeaderInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhSubContLeaderInfoManager()).GetItemByKey(id);
            var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());

            ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", entity.SubContractorID);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/SubContLeaderInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(SubContLeaderInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserCode == null ? "" : UserSession.Current.UserCode.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSubContLeaderInfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/SubContLeaderInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhSubContLeaderInfoManager()).Delete(id);
            if (result)
            {
                (new YhSubContLeaderInfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/SubContLeaderInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(SubContLeaderInfo model)
        {
            if (string.IsNullOrEmpty(model.PhoneNumber) || string.IsNullOrEmpty(model.PhoneNumber))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSubContLeaderInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, SubContLeaderInfoDto query)
        {
            var queryParam = new SubContLeaderInfoDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                LeaderCode = query.LeaderCode,
                LeaderName = query.LeaderName,
                SubContractorID = query.SubContractorID,
                PhoneNumber = query.PhoneNumber,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhSubContLeaderInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}

