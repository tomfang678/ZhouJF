﻿using MB.Framework.Common.Enums;
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
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;

namespace YHPT.Management.WebUI.Controllers
{
    /// <summary>
    /// 道路市政信息
    /// </summary>
    public class RoadMunicipalInfoController : BaseController
    {
        //
        // GET: /RoadMunicipalInfo/

        public ActionResult Index()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);

            return View();
        }
        [MenuItem("~/RoadMunicipalInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", 1);

            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.SubContLeaderInfoID = new SelectList(leaderList, "ID", "LeaderName", 1);

            return View();
        }


        [MenuItem("~/RoadMunicipalInfo/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new YhRoadMunicipalInfoManager()).GetItemByKey(id);
            var roadList = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto());
            this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);

            var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            ViewBag.SubContLeaderInfoID = new SelectList(leaderList, "ID", "LeaderName", entity.SubContLeaderInfoID);

            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/RoadMunicipalInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(RoadMunicipalInfo model, string gridStage)
        {
            model.LastModifiedUser = UserSession.Current.UserCode == null ? "" : UserSession.Current.UserCode.ToString();
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhRoadMunicipalInfoManager()).Update(model);
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
        [MenuItem("~/RoadMunicipalInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhRoadMunicipalInfoManager()).Delete(id);
            if (result)
            {
                (new YhRoadMunicipalInfoManager()).Delete(id);
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/RoadMunicipalInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(RoadMunicipalInfo model)
        {
            if (model.RoadID == 0)
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            model.LastModifiedUser = UserSession.Current.UserCode;
            model.LastModifiedTime = DateTime.Now;
            var result = (new YhRoadMunicipalInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, RoadMunicipalInfoDto query)
        {
            var queryParam = new RoadMunicipalInfoDto
            {
                CurbFlatLength = query.CurbFlatLength,
                CurbSideLength = query.CurbSideLength,
                FastLaneSquare = query.FastLaneSquare,
                FastLaneWidth = query.FastLaneWidth,
                GuideBoardCount = query.GuideBoardCount,
                InspectionShaftCount = query.InspectionShaftCount,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction,
                RoadName = query.RoadName
            };
            var pageList = (new YhRoadMunicipalInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }
    }
}