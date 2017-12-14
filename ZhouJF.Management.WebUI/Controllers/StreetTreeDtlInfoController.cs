using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using System;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 道路行道树
    /// </summary>
    public class StreetTreeDtlInfoController : BaseController
    {
        //
        // GET: /StreetTreeDtlInfo/

        public ActionResult Index()
        {
            return View();
        }
        [MenuItem("~/StreetTreeDtlInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);
            //var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            //ViewBag.SubContLeaderInfoID = new SelectList(leaderList, "ID", "LeaderName", 1);

            //var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());
            //ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", 1);
            //new CommonController().GetDropDownList();

            return View();
        }


        [MenuItem("~/StreetTreeDtlInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhStreetTreeDtlManager()).GetItemByKey(id);
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/StreetTreeDtlInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(StreetTreeDtlInfo model, string gridStage)
        {
            var result = (new YhStreetTreeDtlManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/StreetTreeDtlInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhStreetTreeDtlManager()).Delete(id);
            if (result)
            {
                (new YhStreetTreeDtlManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/StreetTreeDtlInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(StreetTreeDtlInfo model)
        {
            var result = (new YhStreetTreeDtlManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, StreetTreeDtlInfoDto query)
        {
            var queryParam = new StreetTreeDtlInfoDto
            {
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction,
                RoadID = query.RoadID,
                Code=query.Code,
                RoadName=query.RoadName
            };
            var pageList = (new YhStreetTreeDtlManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

    }
}
