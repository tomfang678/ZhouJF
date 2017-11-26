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

namespace YHPT.SystemInfo.Business
{
    public class YhAreainfoManager : IBussiness<AreaInfo, AreaInfoDto>
    {
        private readonly IYhAreainfoDAL _da = new YhAreainfoDAL();

        public int Add(AreaInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public AreaInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<AreaInfo> GetItems(AreaInfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.AreaCode))
                filters.Add(new QueryParameterInfo("AreaCode", data.AreaCode, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.Area))
                filters.Add(new QueryParameterInfo("Area", "%" + data.Area + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.Dept))
                filters.Add(new QueryParameterInfo("Dept", "%" + data.Dept + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.Owner))
                filters.Add(new QueryParameterInfo("Owner", data.Owner, DataFilterConditions.Equal));
            if (data.fromlat > 0)
            {
                filters.Add(new QueryParameterInfo("latitude", data.fromlat, DataFilterConditions.GreaterOrEqual));
                filters.Add(new QueryParameterInfo("latitude", data.tolat, DataFilterConditions.LessOrEqual));
            }
            if (data.fromlng > 0)
            {
                filters.Add(new QueryParameterInfo("longitude", data.fromlng, DataFilterConditions.GreaterOrEqual));
                filters.Add(new QueryParameterInfo("longitude", data.tolng, DataFilterConditions.LessOrEqual));
            }
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<AreaInfo> GetPagedList(AreaInfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.AreaCode))
                        filters.Add(new QueryParameterInfo("AreaCode", data.AreaCode, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.Area))
                        filters.Add(new QueryParameterInfo("Area", "%" + data.Area + "%", DataFilterConditions.Like));
                    if (!string.IsNullOrEmpty(data.Dept))
                        filters.Add(new QueryParameterInfo("Dept", "%" + data.Dept + "%", DataFilterConditions.Like));
                    if (!string.IsNullOrEmpty(data.Owner))
                        filters.Add(new QueryParameterInfo("Owner", data.Owner, DataFilterConditions.Equal));

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
                LogHelper.Error<AreaInfo>("", ex);
            }
            return new PagedList<AreaInfo>();
        }

        public bool Update(AreaInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(AreaInfo model)
        {
            var queryInfo = new AreaInfoDto()
            {
                AreaCode = model.AreaCode
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }


        /// <summary>
        ///   select distinct region from AreaInfo
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> GetAllRegion()
        {
            var entity = _da.GetAllRegion();
            return entity;
        }
    }
}
