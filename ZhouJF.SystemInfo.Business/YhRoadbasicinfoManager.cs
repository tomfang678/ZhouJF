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
using YHPT.SystemInfo.Model.YhAreaInfo;
using YHPT.SystemInfo.Model.YhRoadbasicinfo;

namespace YHPT.SystemInfo.Business
{
    public class YhRoadbasicinfoManager : IBussiness<Roadbasicinfo, RoadbasicinfoDto>
    {
        private readonly IYhRoadbasicinfoDAL _da = new YhRoadbasicinfoDAL();


        public int Add(Roadbasicinfo item)
        {
            return _da.Insert(item, DataBaseResource.Write);
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public Roadbasicinfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Write);
            return entity;
        }

        public List<Roadbasicinfo> GetItems(RoadbasicinfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.RoadCode))
                filters.Add(new QueryParameterInfo("RoadCode", data.RoadCode, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.RoadName))
                filters.Add(new QueryParameterInfo("RoadName", "%" + data.RoadName + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.RoadLevel))
                filters.Add(new QueryParameterInfo("RoadLevel", "%" + data.RoadLevel + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.RoadMaterial))
                filters.Add(new QueryParameterInfo("RoadMaterial", data.RoadMaterial, DataFilterConditions.Equal));
            //if (!string.IsNullOrEmpty(data.AreaCode))
            //    filters.Add(new QueryParameterInfo("AreaCode", data.AreaCode, DataFilterConditions.Equal));

            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Write);
            return result;
        }

        public PagedList<Roadbasicinfo> GetPagedList(RoadbasicinfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.RoadCode))
                        filters.Add(new QueryParameterInfo("RoadCode", data.RoadCode, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.RoadName))
                        filters.Add(new QueryParameterInfo("RoadName", "%" + data.RoadName + "%", DataFilterConditions.Like));
                    if (!string.IsNullOrEmpty(data.RoadLevel))
                        filters.Add(new QueryParameterInfo("RoadLevel", "%" + data.RoadLevel + "%", DataFilterConditions.Like));
                    if (!string.IsNullOrEmpty(data.RoadMaterial))
                        filters.Add(new QueryParameterInfo("RoadMaterial", data.RoadMaterial, DataFilterConditions.Equal));
                    if (data.AreaInfoID >= 0)
                        filters.Add(new QueryParameterInfo("AreaInfoID", data.AreaInfoID, DataFilterConditions.Equal));
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
                LogHelper.Error<Roadbasicinfo>("", ex);
            }
            return new PagedList<Roadbasicinfo>();
        }

        public bool Update(Roadbasicinfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(Roadbasicinfo model)
        {
            var queryInfo = new RoadbasicinfoDto()
            {
                RoadCode = model.RoadCode
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
