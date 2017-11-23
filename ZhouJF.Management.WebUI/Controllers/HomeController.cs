using SmartFast.Web.BaseLibrary;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;

namespace YHPT.Management.WebUI.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (Session["LoginTime"] == null)
            {
                Session["LoginTime"] = DateTime.Now;
            }
            ViewBag.LoginTime = ((DateTime)Session["LoginTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            return View();
        }

        [HttpPost]
        public ActionResult GetSearchTips()
        {
            //var result = ServiceContext.Current.HouseService.GetHouseMapList(new HouseRequest()).Select(a => new
            //{
            //    Id = a.ID,
            //    Lng = a.Longitude,
            //    Lat = a.Latitude,
            //    Address = a.Address,
            //    MinRental = a.Rent,
            //    Name = a.MainTitle,
            //    FilterCount = 0,
            //    Cfg = 0,
            //    Order = 1,
            //    ExtendURL = a.Address,
            //    Relaxation = "",
            //    BusinessArea = a.AreaName,
            //    Type = 0,
            //    RoomCount = a.House_Room.Count
            //}).ToList();

            //var model = new
            //{
            //    HasResult = true,
            //    Communities = result
            //};
            //return Json(model);
            //cityid=
            //   {"Name":"青羊区","Lng":104.024661,"Lat":30.675595,"Type":1}
            var result = new[]{
                new{
                Name="江北区测试1",
                Lng=106.578900,
                Lat=29.607854,
                Type=0
                },
                new{
                Name="江北区测试2",
                Lng=106.578670,
                Lat=29.607114,
                Type=0
                }
            };
            return Json(result);
        }

        /// <summary>
        /// 根据经纬度范围查找房源信息
        /// </summary>
        /// <param name="fromlng">开始经度</param>
        /// <param name="tolng">结束经度</param>
        /// <param name="fromlat">开始纬度</param>
        /// <param name="tolat">结束纬度</param>
        /// <param name="price">价格</param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchCommunities()
        {
            //request.PageSize = 10000;
            //var result = ServiceContext.Current.HouseService.GetHouseMapList(request).Select(a => new
            //{
            //    Id = a.ID,
            //    Lng = a.Longitude,
            //    Lat = a.Latitude,
            //    Address = a.Address,
            //    MinRental = a.Rent,
            //    Name = a.MainTitle,
            //    FilterCount = 0,
            //    Cfg = 0,
            //    Order = 1,
            //    ExtendURL = a.Address,
            //    Relaxation = "",
            //    BusinessArea = a.AreaName,
            //    Type = 0,
            //    RoomCount = a.House_Room.Where(t => t.State == true).ToList().Count
            //}).ToList();

            //var model = new
            //{
            //    HasResult = true,
            //    Communities = result.Where(t => t.RoomCount > 0).ToList()
            //};
            return Json(null);
        }


        [HttpGet]
        public ActionResult GetRegions()
        {
            var molde = new[]{
                new  {
                Name = "南汇",
                Lng = "121.76",
                Lat = "31.05",
                Type = "0"
            },
           new  {
                Name = "宝山",
                Lng = "121.48",
                Lat = "31.41",
                Type = "0"
            },
             new  {
                Name = "奉贤",
                Lng = "121.46",
                Lat = "30.92",
                Type = "0"
            },
             new  {
                Name = "崇明",
                Lng = "121.40",
                Lat = "31.73",
                Type = "0"
            },
             new  {
                Name = "松江",
                Lng = "121.24",
                Lat = "31.00",
                Type = "0"
            },
             new {
                Name = "嘉定",
                Lng = "121.24",
                Lat = "31.40",
                Type = "0"
            },
             new {
                Name = "金山",
                Lng = "121.16",
                Lat = "30.89",
                Type = "0"
            },
             new {
                Name = "青浦",
                Lng = "121.10",
                Lat = "31.15",
                Type = "0"
            },
              new {
                Name = "黄浦区",
                Lng = "121.48",
                Lat = "31.23",
                Type = "0"
            },
             new {
                Name = "浦东新区",
                Lng = "121.53 ",
                Lat = "31.22",
                Type = "0"
            }
            };
            return Json(molde, JsonRequestBehavior.AllowGet);
        }

    }
}
