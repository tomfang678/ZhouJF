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
using YHPT.SystemInfo.Model.YhRoadMunicipalInfo;

namespace YHPT.SystemInfo.Business
{
    public class YhRoadMunicipalInfoManager : IBussiness<RoadMunicipalInfo, RoadMunicipalInfoDto>
    {
        private readonly IYhRoadMunicipalInfoDAL _da = new YhRoadMunicipalInfoDAL();

        public int Add(RoadMunicipalInfo item)
        {
            if (CheckCode(item))
            {
                return _da.Insert(item, DataBaseResource.Read);
            }
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }
        public List<RoadMunicipalInfo> GetItemByRoadId(int roadId)
        {
            var entity = _da.getByRoadId(roadId);
            return entity;
        }


        public RoadMunicipalInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<RoadMunicipalInfo> GetItems(RoadMunicipalInfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.LeaderCode))
                filters.Add(new QueryParameterInfo("LeaderCode", data.LeaderCode, DataFilterConditions.Equal));
            if (data.RoadLength != null && data.RoadLength != 0)
                filters.Add(new QueryParameterInfo("RoadLength", data.RoadLength, DataFilterConditions.Equal));
            if (data.RoadSquare != null && data.RoadSquare != 0)
                filters.Add(new QueryParameterInfo("RoadSquare", data.RoadSquare, DataFilterConditions.Equal));
            if (data.SlowLaneWidth != null && data.SlowLaneWidth != 0)
                filters.Add(new QueryParameterInfo("SlowLaneWidth", data.SlowLaneWidth, DataFilterConditions.Equal));
            if (data.SlowLaneSquare != null && data.SlowLaneSquare != 0)
                filters.Add(new QueryParameterInfo("SlowLaneSquare", data.SlowLaneSquare, DataFilterConditions.Equal));
            if (data.FastLaneWidth != null && data.FastLaneWidth != 0)
                filters.Add(new QueryParameterInfo("FastLaneWidth", data.FastLaneWidth, DataFilterConditions.Equal));
            if (data.RoadID >= 0)
                filters.Add(new QueryParameterInfo("RoadID", data.RoadID, DataFilterConditions.Equal));

            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<RoadMunicipalInfo> GetPagedList(RoadMunicipalInfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.LeaderCode))
                        filters.Add(new QueryParameterInfo("LeaderCode", data.LeaderCode, DataFilterConditions.Equal));
                    if (data.RoadLength != null && data.RoadLength != 0)
                        filters.Add(new QueryParameterInfo("RoadLength", data.RoadLength, DataFilterConditions.Equal));
                    if (data.RoadSquare != null && data.RoadSquare != 0)
                        filters.Add(new QueryParameterInfo("RoadSquare", data.RoadSquare, DataFilterConditions.Equal));
                    if (data.SlowLaneWidth != null && data.SlowLaneWidth != 0)
                        filters.Add(new QueryParameterInfo("SlowLaneWidth", data.SlowLaneWidth, DataFilterConditions.Equal));
                    if (data.SlowLaneSquare != null && data.SlowLaneSquare != 0)
                        filters.Add(new QueryParameterInfo("SlowLaneSquare", data.SlowLaneSquare, DataFilterConditions.Equal));
                    if (data.FastLaneWidth != null && data.FastLaneWidth != 0)
                        filters.Add(new QueryParameterInfo("FastLaneWidth", data.FastLaneWidth, DataFilterConditions.Equal));
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
                LogHelper.Error<RoadMunicipalInfo>("", ex);
            }
            return new PagedList<RoadMunicipalInfo>();
        }

        public bool Update(RoadMunicipalInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(RoadMunicipalInfo model)
        {
            var queryInfo = new RoadMunicipalInfoDto()
            {
                RoadID = model.RoadID
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
