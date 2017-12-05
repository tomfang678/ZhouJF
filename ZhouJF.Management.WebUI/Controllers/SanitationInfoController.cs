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
    /// 道路环卫信息
    /// </summary>
    public class SanitationInfoController : BaseController
    {
        //
        // GET: /SanitationInfo/

        public ActionResult Index()
        {
            return View();
        }
        [MenuItem("~/SanitationInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.SubContLeaderInfoID = new SelectList(leaderList, "ID", "LeaderName", 1);
            return View();
        }


        [MenuItem("~/SanitationInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhSanitationInfoManager()).GetItemByKey(id);
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);
            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            
            ViewBag.SubContLeaderInfoID = new SelectList(leaderList, "ID", "LeaderName", entity.SubContLeaderInfoID);

            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/SanitationInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(SanitationInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserCode == null ? "" : UserSession.Current.UserCode.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSanitationInfoManager()).Update(model);
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
        [MenuItem("~/SanitationInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhSanitationInfoManager()).Delete(id);
            if (result)
            {
                (new YhSanitationInfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/SanitationInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(SanitationInfo model)
        {
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhSanitationInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, SanitationInfoDto query)
        {
            var queryParam = new SanitationInfoDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                ID = query.ID,
                LeaderCode = query.LeaderCode,
                SortField = "RoadID",
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhSanitationInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}
