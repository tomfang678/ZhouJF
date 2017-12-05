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
                    enity.ImgInfoList = (new YhImgInfoManager()).GetImgByRoadId(id);
                }
                ViewBag.entity = enity;
                if (enity.ImgInfoList != null)
                {
                    ViewBag.RoadBasicInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("RoadBasicInfo")).ToList();
                    ViewBag.BridgeInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("BridgeInfo")).ToList();
                    ViewBag.RoadMunicipalInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("RoadMunicipalInfo")).ToList();
                    ViewBag.SanitationInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("SanitationInfo")).ToList();
                    ViewBag.SewerInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("SewerInfo")).ToList();
                    ViewBag.StreetTreeInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("StreetTreeInfo")).ToList();
                    ViewBag.GreenLandInfoImgList = enity.ImgInfoList.Where(t => t.imgModule.Equals("GreenLandInfo")).ToList();
                }
                else
                {
                    ViewBag.RoadBasicInfoImgList = null;
                    ViewBag.BridgeInfoImgList = null;
                    ViewBag.RoadMunicipalInfoImgList = null;
                    ViewBag.SanitationInfoImgList = null;
                    ViewBag.SewerInfoImgList = null;
                    ViewBag.StreetTreeInfoImgList = null;
                    ViewBag.GreenLandInfoImgList = null;
                }
            }
            return View(enity);

        }

        public ActionResult Index()
        {
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            AreaInfo allArea = new AreaInfo { 
                ID = -1,
                Area = "所有"
            };
            areaList.Add(allArea);
            ViewBag.AreaInfoID = new SelectList(areaList, "ID", "Area", 1);

            return View();
        }
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            var dropList = new SelectList(areaList, "ID", "Area", 1);
            ViewData["dropList"] = dropList;
            return View();
        }


        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhRoadbasicinfoManager()).GetItemByKey(id);
            var areaList = (new YhAreainfoManager()).GetItems(new AreaInfoDto());
            ViewBag.AreaInfoID = new SelectList(areaList, "ID", "Area", entity.AreaInfoID);

            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/RoadBasicInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(Roadbasicinfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserCode == null ? "" : UserSession.Current.UserCode.ToString();
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
                AreaInfoID = query.AreaInfoID,
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
