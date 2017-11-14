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
using YHPT.SystemInfo.Model;
using YHPT.SystemInfo.Model.BfUser;
using YHPT.SystemInfo.Model.Subcontractor;

namespace YHPT.Management.WebUI.Controllers
{

    /// <summary>
    /// 分包方管理
    /// </summary>
    public class SubContractorInfoController : BaseController
    {
        //
        // GET: /SubContractorInfo/
        public ActionResult Index()
        {
            return View();
        }

        [MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            return View();
        }


        [MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhSubcontractorinfoManager()).GetItemByKey(id);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(SubContractorInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID == null ? "" : UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSubcontractorinfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhSubcontractorinfoManager()).Delete(id);
            if (result)
            {
                (new YhSubcontractorinfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/SubContractorInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(SubContractorInfo model)
        {
            if (string.IsNullOrEmpty(model.SubContractorCode) || string.IsNullOrEmpty(model.SubContractorCode))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSubcontractorinfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, SubContractorDto query)
        {
            var queryParam = new SubContractorDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SubContractorBoss = query.SubContractorBoss,
                SubContractorCode = query.SubContractorCode,
                SubContractorCrop = query.SubContractorCrop,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhSubcontractorinfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}
