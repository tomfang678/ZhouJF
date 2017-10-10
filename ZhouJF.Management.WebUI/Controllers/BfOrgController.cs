using System;
using System.Web.Mvc;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfOrg;

namespace YHPT.Management.WebUI.Controllers
{
    public class BfOrgController : BaseController
    {
        //
        // GET: /BfOrg/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryBfOrg(JQueryDataTablesModel jQueryDataTablesModel, QueryBfOrgDto query)
        {
            var queryParam = new QueryBfOrgDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                CODE = query.CODE,
                NAME = query.NAME,
                OWNER_ID = query.OWNER_ID,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new BfOrgManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

        [MenuItem("~/BfOrg/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new BfOrgManager()).GetItemByKey(id);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/BfOrg/Index", AuthorizeKey.Update)]
        public JsonResult Edit(BfOrgInfo model, string gridStage)
        {
            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfOrgManager()).Update(model);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
        }

        [HttpPost]
        [MenuItem("~/BfOrg/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new BfOrgManager()).Delete(id);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [MenuItem("~/BfOrg/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [MenuItem("~/BfOrg/Index", AuthorizeKey.Add)]
        public JsonResult Add(BfOrgInfo model)
        {
            if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.NAME))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.LAST_MODIFIED_USER = UserSession.Current.UserID;
            model.LAST_MODIFIED_DATE = DateTime.Now;
            var result = (new BfOrgManager()).Add(model);
            if (result > 0)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
        }


    }
}
