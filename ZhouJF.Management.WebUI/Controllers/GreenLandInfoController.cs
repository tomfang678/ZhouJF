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
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 道路绿化信息
    /// </summary>
    public class GreenLandInfoController : BaseController
    {
        //
        // GET: /GreenLandInfo/

        public ActionResult Index()
        {
            return View();
        }
        [MenuItem("~/GreenLandInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", 1);


            return View();
        }


        [MenuItem("~/GreenLandInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhGreenLandInfoManager()).GetItemByKey(id);
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", entity.LeaderCode);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/GreenLandInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(GreenLandInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhGreenLandInfoManager()).Update(model);
            if (result)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, Message = "编辑失败" });
            }
        }

        [HttpPost]
        [MenuItem("~/GreenLandInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhGreenLandInfoManager()).Delete(id);
            if (result)
            {
                (new YhGreenLandInfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/GreenLandInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(GreenLandInfo model)
        {
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhGreenLandInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, GreenLandInfoDto query)
        {
            var queryParam = new GreenLandInfoDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                ID = query.ID,
                LeaderCode = query.LeaderCode,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhGreenLandInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}
