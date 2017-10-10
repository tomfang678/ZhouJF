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
    public class StreetTreeInfoController : BaseController
    {
        //
        // GET: /StreetTreeInfo/

        public ActionResult Index()
        {
            return View();
        }
        [MenuItem("~/StreetTreeInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);

            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", 1);

            //var cmpList = (new YhSubcontractorinfoManager()).GetItems(new SubContractorDto());
            //ViewBag.SubContractorID = new SelectList(cmpList, "ID", "SubContractorCrop", 1);
            //new CommonController().GetDropDownList();

            return View();
        }


        [MenuItem("~/StreetTreeInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhStreetTreeInfoManager()).GetItemByKey(id);

            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);

            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", entity.LeaderCode);

            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/StreetTreeInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(StreetTreeInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhStreetTreeInfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/StreetTreeInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhStreetTreeInfoManager()).Delete(id);
            if (result)
            {
                (new YhStreetTreeInfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/StreetTreeInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(StreetTreeInfo model)
        {
            //if (string.IsNullOrEmpty(model.RoadCode) || string.IsNullOrEmpty(model.RoadCode))
            //{
            //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            //}
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhStreetTreeInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, StreetTreeInfoDto query)
        {
            var queryParam = new StreetTreeInfoDto
            {
                LeaderCode = query.LeaderCode,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhStreetTreeInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

    }
}
