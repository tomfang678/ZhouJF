using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using YHPT.SystemInfo.Model.YhManager;
using YHPT.SystemInfo.DAL;

namespace YHPT.Management.WebUI.Controllers
{
    public class ImgInfoController : BaseController
    {
        //
        // GET: /Img/

        public ActionResult Index(int moduleId)
        {
            var entity = (new YhImgInfoManager()).GetImgByModule(moduleId);
            return View(entity);
        }

        [MenuItem("~/ImgInfo/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            var imgTypeList = (new BfCodeInfoDAL()).GetBfCodeByModuleId("ImgType");
            ViewBag.ImgType = new SelectList(imgTypeList, "Code", "Name", 1);
            return View();
        }


        [MenuItem("~/ImgInfo/Index", AuthorizeKey.Update)]
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
        [MenuItem("~/ImgInfo/Index", AuthorizeKey.Update)]
        public JsonResult Edit(ImgInfo model, string gridStage)
        {
            model.CreateUser = UserSession.Current.UserID.ToString();
            model.CreateTime = DateTime.Now;
            var result = (new YhImgInfoManager()).Update(model);
            return Json(new ResponseMessage() { IsSuccess = true });
        }

        [HttpPost]
        [MenuItem("~/ImgInfo/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new YhImgInfoManager()).Delete(id);
            if (result)
            {
                return Json(new ResponseMessage() { IsSuccess = true });
            }
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [HttpPost]
        [MenuItem("~/ImgInfo/Index", AuthorizeKey.Add)]
        public JsonResult Add(ImgInfo model)
        {
            //if (string.IsNullOrEmpty(model.RoadCode) || string.IsNullOrEmpty(model.RoadCode))
            //{
            //    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            //}
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
