using SmartFast.Web.BaseLibrary;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;

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
            var result = new[]{
                new{
                Name="上海地区1",
                Lng=121.48100,
                Lat=31.23300,
                Type=0
                },
                new{
                Name="上海地区2",
                Lng=121.48300,
                Lat=31.23500,
                Type=0
                }
            };
            return Json(result);
        }

        /// <summary>
        /// 根据经纬度范围查找区域信息
        /// </summary>
        /// <param name="fromlng">开始经度</param>
        /// <param name="tolng">结束经度</param>
        /// <param name="fromlat">开始纬度</param>
        /// <param name="tolat">结束纬度</param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchCommunities(AreaInfoDto queryMOdel)
        {
            //将对象转换成 地图可以展示的类型
            List<AreaInfo> result = (new YhAreainfoManager()).GetItems(queryMOdel);
            List<object> objList = new List<object>();
            result.ForEach(a =>
            {
                objList.Add(new
                {
                    Id = a.ID,
                    Lng = a.longitude,
                    Lat = a.latitude,
                    Address = string.Format("[业主]：{0}  [标段]：{1}  [项目部]：{2}  [区域]：{3}", a.Owner, a.Section, a.Dept, a.Area),
                    MinRental = a.Dept,
                    Name = a.Area + "(" + a.Owner + a.Section + ")",
                    FilterCount = 0,
                    Cfg = 0,
                    Order = 1,
                    ExtendURL = "",
                    Relaxation = "",
                    BusinessArea = "",
                    Type = 0,
                    RoomCount = a.v1
                }
             );
            });
            var model = new
            {
                HasResult = true,
                Communities = objList
            };
            return Json(model);
        }

        /// <summary>
        /// 根据区域ID或
        /// </summary>
        /// <param name="communityIds"></param>
        /// <param name="price"></param>
        /// <param name="cfg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SearchRooms(int communityIds, int price = 1, int cfg = 1)
        {
            if (communityIds > 0)
            {
                List<Roadbasicinfo> result = (new YhRoadbasicinfoManager()).GetItems(new RoadbasicinfoDto { AreaInfoID = communityIds });
                var jsonmodel = new
                {
                    Rooms = result,
                    RoomCount = result.Count
                };
                return Json(jsonmodel);
            }
            else
            {
                return Json(new
                {
                    Rooms = new { },
                    RoomCount = 0
                });
            }
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
