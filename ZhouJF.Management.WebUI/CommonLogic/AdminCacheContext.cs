//using SmartFast.Plugin.Cache;
//using SmartFast.Web.BaseLibrary;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace YHPT.Management.WebUI.CommonLogic
//{
//    public class AdminCacheContext
//    {
//        public static AdminCacheContext Current
//        {
//            get
//            {
//                return CacheHelper.GetItem<AdminCacheContext>();
//            }
//        }

//        public Dictionary<int, City> CityDic
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, City>>("CityDic", () =>
//                {
//                    return ServiceContext.Current.CrmService.GetCityList().ToDictionary(a => a.ID);
//                });
//            }
//        }

//        public Dictionary<int, Area> AreaDic
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Area>>("AreaDic", () =>
//                {
//                    return ServiceContext.Current.CrmService.GetAreaList().ToDictionary(a => a.ID);
//                });
//            }
//        }

//        public static Dictionary<string, ICollection<BfCodeDetail>> GetSysParma
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<string, ICollection<BfCodeDetail>>>("Smart_SysParamKey", () =>
//                {
//                    return ServiceContext.Current.BfCodeService.GetSysParamByCode();
//                });
//            }
//        }

//        public static IEnumerable<AD> GetADList
//        {
//            get
//            {
//                return CacheHelper.Get<IEnumerable<AD>>("Smart_AD", () =>
//                {
//                    return ServiceContext.Current.ADService.GetList();
//                });
//            }
//        }
//        public static IEnumerable<AD> GetADListBySql
//        {
//            get
//            {
//                return CacheHelper.Get<IEnumerable<AD>>("Smart_AD_SQL", () =>
//                {
//                    return ServiceContext.Current.ADService.GetListBySql();
//                });
//            }
//        }

//        public static IEnumerable<House_Main> GetRecommandHouseList
//        {
//            get
//            {
//                return CacheHelper.Get<IEnumerable<House_Main>>("Smart_RecommandHouseList", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetRecommandHouseList(new HouseRequest { PageSize = 16 });
//                });
//            }
//        }

//        public static Dictionary<int, Region> Region
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_Region", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetRegion().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }
//        public static Dictionary<int, Region> Area
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_Area", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetRegion().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }

//        public static Dictionary<int, Region> Provice
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_Provice", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetProvice().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }
//        public static Dictionary<int, Region> City
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_City", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetCity().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }
//        public static Dictionary<int, Region> QuYu
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_QuYu", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetAwaliableQuyu().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }

//        public static Dictionary<int, Region> HotQuYu
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_QuYu", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetAwaliableQuyu(new RegionRequest { topNum = 8 }).ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }
//        public static Dictionary<int, Region> County
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Region>>("Smart_County", () =>
//                {
//                    return ServiceContext.Current.HouseService.GetCounty().ToDictionary(a => a.REGION_ID);
//                });
//            }
//        }
//        public static Dictionary<int, Staff> Staffs
//        {
//            get
//            {
//                return CacheHelper.Get<Dictionary<int, Staff>>("Smart_Staff", () =>
//                {
//                    return ServiceContext.Current.OAService.GetStaffList().ToDictionary(a => a.ID);
//                });
//            }
//        }
//    }
//}
