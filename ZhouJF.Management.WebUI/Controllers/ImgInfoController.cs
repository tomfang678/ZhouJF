using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.DAL;
using YHPT.Management.WebUI.CommonLogic;
using SmartFast.BaseFrame.Utility;

namespace YHPT.Management.WebUI.Controllers
{
    public class ImgInfoController : BaseController
    {
        //
        // GET: /Img/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string moule, int moduleId, int RoadID)
        {
            var entity = (new YhImgInfoManager()).GetImgByModule(moule, moduleId);
            ViewBag.imgList = entity;
            ViewBag.imgModuleList = new SelectList(CommonFeild.GetModuleList(), "code", "name", moule);
            var imgTypeList = (new BfCodeInfoDAL()).GetBfCodeByModuleId("ImgType");
            ViewBag.ImgType = new SelectList(imgTypeList, "Code", "Name", 1);
            ViewBag.imgModuleId = moduleId;
            ViewBag.imgModule = moule;
            ViewBag.imgModuleName = CommonFeild.GetModuleName(moule);
            ViewBag.RoadID = RoadID;
            return View(entity);
        }

        public ActionResult Add()
        {
            var imgTypeList = (new BfCodeInfoDAL()).GetBfCodeByModuleId("ImgType");
            ViewBag.ImgType = new SelectList(imgTypeList, "Code", "Name", 1);
            return View();
        }


        public ActionResult Edit(int id)
        {
            var entity = (new YhImgInfoManager()).GetItemByKey(id);
            //var roadList = (new YhImgInfoManager()).GetItems(new ImgInfoDto()); 
            //this.ViewBag.RoadID = new SelectList(roadList, "ID", "RoadName", entity.RoadID);
            //var leaderList = (new YhSubContLeaderInfoManager()).GetItems(new SubContLeaderInfoDto());
            //ViewBag.LeaderCode = new SelectList(leaderList, "LeaderCode", "LeaderName", entity.LeaderCode);
            return View(entity);
        }

        [HttpPost]
        public JsonResult Edit(ImgInfo model, string gridStage)
        {
            model.CreateUser = UserSession.Current.UserID.ToString();
            model.CreateTime = DateTime.Now;
            var result = (new YhImgInfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var entity = (new YhImgInfoManager()).GetItemByKey(id);
                if (!string.IsNullOrEmpty(entity.imgUrl))
                {
                    FileHelper.ImgFileRemoe(entity.imgUrl);
                }
                var result = (new YhImgInfoManager()).Delete(id);
                if (result)
                {
                    return Json(new ResponseMessage() { IsSuccess = true });
                }
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
            }
            catch (Exception e)
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" + e.Message });
            }
        }

        [HttpPost]
        //[MenuItem("~/ImgInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(ImgInfo model)
        {
            model.CreateUser = UserSession.Current.UserCode;
            model.CreateTime = DateTime.Now;
            var result = (new YhImgInfoManager()).Add(model);
            if (result > 0)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            else
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
            }
        }


        public ActionResult QueryDB(JQueryDataTablesModel jQueryDataTablesModel, ImgInfoDto query)
        {
            var queryParam = new ImgInfoDto
            {
                imgModuleId = query.imgModuleId,
                imgType = query.imgType,
                StartIndex = query.StartIndex,
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new YhImgInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

    }
}
