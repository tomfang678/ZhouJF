using System;
using System.Collections.Generic;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.Common.Model;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfUser;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.SystemInfo.Business
{
    public class YhBridgeinfoManager : IBussiness<BridgeInfo, BridgeinfoDto>
    {
        private readonly IYhBridgeInfoDAL _da = new YhBridgeInfoDAL();
        public List<BridgeInfo> GetItemByRoadId(int roadId)
        {
            var entity = _da.getByRoadId(roadId);
            return entity;
        }
        public int Add(BridgeInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BridgeInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BridgeInfo> GetItems(BridgeinfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.RoadID != null && data.RoadID != 0)
                filters.Add(new QueryParameterInfo("RoadID", data.RoadID, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.LeaderCode))
                filters.Add(new QueryParameterInfo("LeaderCode", data.LeaderCode, DataFilterConditions.Equal));

            //if (!string.IsNullOrEmpty(data.RoadCode))
            //    filters.Add(new QueryParameterInfo("RoadCode", data.RoadCode, DataFilterConditions.Equal));
            //if (!string.IsNullOrEmpty(data.RoadName))
            //    filters.Add(new QueryParameterInfo("RoadName", "%" + data.RoadName + "%", DataFilterConditions.Like));
            //if (!string.IsNullOrEmpty(data.RoadLevel))
            //    filters.Add(new QueryParameterInfo("RoadLevel", "%" + data.RoadLevel + "%", DataFilterConditions.Like));
            //if (!string.IsNullOrEmpty(data.RoadMaterial))
            //    filters.Add(new QueryParameterInfo("RoadMaterial", data.RoadMaterial, DataFilterConditions.Equal));
            //if (!string.IsNullOrEmpty(data.AreaCode))
            //    filters.Add(new QueryParameterInfo("AreaCode", data.AreaCode, DataFilterConditions.Equal));

            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BridgeInfo> GetPagedList(BridgeinfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (data.RoadID != null && data.RoadID != 0)
                        filters.Add(new QueryParameterInfo("RoadID", data.RoadID, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.LeaderCode))
                        filters.Add(new QueryParameterInfo("LeaderCode", data.LeaderCode, DataFilterConditions.Equal));
                    //if (!string.IsNullOrEmpty(data.RoadCode))
                    //    filters.Add(new QueryParameterInfo("RoadCode", data.RoadCode, DataFilterConditions.Equal));
                    //if (!string.IsNullOrEmpty(data.RoadName))
                    //    filters.Add(new QueryParameterInfo("RoadName", "%" + data.RoadName + "%", DataFilterConditions.Like));
                    //if (!string.IsNullOrEmpty(data.RoadLevel))
                    //    filters.Add(new QueryParameterInfo("RoadLevel", "%" + data.RoadLevel + "%", DataFilterConditions.Like));
                    //if (!string.IsNullOrEmpty(data.RoadMaterial))
                    //    filters.Add(new QueryParameterInfo("RoadMaterial", data.RoadMaterial, DataFilterConditions.Equal));
                    //if (!string.IsNullOrEmpty(data.AreaCode))
                    //    filters.Add(new QueryParameterInfo("AreaCode", data.AreaCode, DataFilterConditions.Equal));
                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<SubContractorInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BridgeInfo>("", ex);
            }
            return new PagedList<BridgeInfo>();
        }

        public bool Update(BridgeInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BridgeInfo model)
        {
            var queryInfo = new BridgeinfoDto()
            {
                LeaderCode = model.LeaderCode
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }
    }
}
