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
using YHPT.SystemInfo.Model.YhRoadbasicinfo;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 道路基本信息
    /// </summary>
    public class RoadBasicInfoController : BaseController
    {
        //
        // GET: /RoadBasicInfo/  YhRoadbasicinfoManager
        public ActionResult Detail(int id)
        {
            Roadbasicinfo enity = new Roadbasicinfo();
            if (id > 0)
            {
                enity = (new YhRoadbasicinfoManager()).GetItemByKey(id);
                if (enity != null)
                {
                    enity.RoadMunicipalInfoList = (new YhRoadMunicipalInfoManager()).GetItemByRoadId(id);
                    enity.SanitationInfoList = (new YhSanitationInfoManager()).GetItemByRoadId(id);
                    enity.GenLandInfoList = (new YhGreenLandInfoManager()).GetItemByRoadId(id);
                    enity.SewerinfoInfoList = (new YhSewerinfoInfoManager()).GetItemByRoadId(id);
                    enity.BridgeInfoList = (new YhBridgeinfoManager()).GetItemByRoadId(id);
                    enity.StreetTreeInfoList = (new YhStreetTreeInfoManager()).GetItemByRoadId(id);
                }
                ViewBag.entity = enity;
            }
            return View(enity);

        }

        public ActionResult Index()
        {
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            ViewBag.AreaCode = new SelectList(areaList, "AreaCode", "Area", 1);
            return View();
        }
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            ViewBag.AreaCode = new SelectList(areaList, "AreaCode", "Area", 1);
            return View();
        }


        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhRoadbasicinfoManager()).GetItemByKey(id);
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            ViewBag.AreaCode = new SelectList(areaList, "AreaCode", "Area", entity.AreaCode);

            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(Roadbasicinfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserID == null ? "" : UserSession.Current.UserID.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhRoadbasicinfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhRoadbasicinfoManager()).Delete(id);
            if (result)
            {
                (new YhRoadbasicinfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(Roadbasicinfo model)
        {
            if (string.IsNullOrEmpty(model.RoadCode) || string.IsNullOrEmpty(model.RoadCode))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhRoadbasicinfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, RoadbasicinfoDto query)
        {
            var queryParam = new RoadbasicinfoDto
            {
                ID = query.ID,
                AreaCode = query.AreaCode,
                BridgeNumber = query.BridgeNumber,
                RoadCode = query.RoadCode,
                RoadLevel = query.RoadLevel,
                RoadMaterial = query.RoadMaterial,
                RoadName = query.RoadName,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhRoadbasicinfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}
