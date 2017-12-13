using System;
using System.Collections.Generic;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.Model.YhManager;


namespace YHPT.SystemInfo.Business
{
    public class YhStreetTreeDtlManager : IBussiness<StreetTreeDtlInfo, StreetTreeDtlInfoDto>
    {
        private readonly IYhStreetTreeDtlDAL _da = new YhStreetTreeDtlDAL();
        public List<StreetTreeDtlInfo> GetItemByRoadId(int roadId)
        {
            var entity = _da.getByRoadId(roadId);
            return entity;
        }

        public int Add(StreetTreeDtlInfo item)
        {
            return _da.Insert(item, DataBaseResource.Read);
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public StreetTreeDtlInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<StreetTreeDtlInfo> GetItems(StreetTreeDtlInfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.RoadID != 0)
                filters.Add(new QueryParameterInfo("RoadID", data.RoadID, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.Code))
                filters.Add(new QueryParameterInfo("Code", data.Code, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
                if (queryParameterInfos != null)
                {
                    filters.AddRange(queryParameterInfos);
                }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<StreetTreeDtlInfo> GetPagedList(StreetTreeDtlInfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (data.RoadID != null && data.RoadID > 0)
                        filters.Add(new QueryParameterInfo("RoadID", data.RoadID, DataFilterConditions.Equal));
                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<StreetTreeDtlInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<StreetTreeDtlInfo>("", ex);
            }
            return new PagedList<StreetTreeDtlInfo>();
        }

        public bool Update(StreetTreeDtlInfo item)
        {
            //if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            //return false;
        }

        public bool CheckCode(StreetTreeDtlInfo model)
        {
            var queryInfo = new StreetTreeDtlInfoDto()
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
