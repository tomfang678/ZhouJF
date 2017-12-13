using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.CommonLogic
{
    public class CommonFeild
    {
        public static readonly string RoadbasicInfo = "RoadBasicInfo";
        public static readonly string BridgeInfo = "BridgeInfo";
        public static readonly string RoadmunicipalInfo = "RoadMunicipalInfo";
        public static readonly string SanitationInfo = "SanitationInfo";
        public static readonly string Sewerinfo = "SewerInfo";
        public static readonly string Streettree = "StreetTreeInfo";
        public static readonly string GreenLandInfo = " GreenLandInfo";

        public static string GetModuleName(string module)
        {
            if (module == "RoadBasicInfo")
                return "道路基本信息";
            if (module == "BridgeInfo")
                return "桥梁信息";
            if (module == "RoadMunicipalInfo")
                return "市政信息";
            if (module == "SanitationInfo")
                return "环卫信息";
            if (module == "SewerInfo")
                return "下水道信息";
            if (module == "StreetTreeInfo")
                return "行道树";
            if (module == "StreetTreeDtlInfo")
                return "行道树明细";
            if (module == "GreenLandInfo")
                return "绿化信息";
            return "";
        }

        public static List<object> GetModuleList()
        {
            return new List<object>
            {
                new {
                code="RoadBasicInfo",
                name="道路基本信息"
                },
                new {
                code="BridgeInfo",
                name="桥梁信息"
                },
                new {
                code="RoadMunicipalInfo",
                name="市政信息"
                },
                new {
                code="SanitationInfo",
                name="环卫信息"
                },
                new {
                code="SewerInfo",
                name="下水道信息"
                },
                new {
                code="GreenLandInfo",
                name="绿化信息"
                },
                 new {
                code="StreetTreeInfo",
                name="行道树"
                },
                 new {
                code="StreetTreeDtlInfo",
                name="行道树明细"
                }
            };
        }


        public static List<object> GetAreaList()
        {
            return new List<object>
           {
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

        }
    }
}