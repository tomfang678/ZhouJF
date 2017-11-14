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
    /// 道路桥梁信息
    /// </summary>
    public class BridgeInfoController : BaseController
    {
        //
        // GET: /BridgeInfo/

        public ActionResult Index()
        {
            return View();
        }
        [MenuItem("~/BridgeInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", 1);

            return View();
        }


        [MenuItem("~/BridgeInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhBridgeinfoManager()).GetItemByKey(id);

            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", entity.LeaderCode);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/BridgeInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(BridgeInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID == null ? "" : UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhBridgeinfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/BridgeInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhBridgeinfoManager()).Delete(id);
            if (result)
            {
                (new YhBridgeinfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/BridgeInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(BridgeInfo model)
        {
            //if (string.IsNullOrEmpty(model.) || string.IsNullOrEmpty(model.RoadCode))
            //{
            //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            //}
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhBridgeinfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, BridgeinfoDto query)
        {
            var queryParam = new BridgeinfoDto
            {
                ID = query.ID,
                LeaderCode = query.LeaderCode,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhBridgeinfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

    }
}
