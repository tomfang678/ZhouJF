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
using YHPT.SystemInfo.Model.YhManager;

namespace YHPT.SystemInfo.Business
{
    public class YhSubContLeaderInfoManager : IBussiness<SubContLeaderInfo, SubContLeaderInfoDto>
    {
        private readonly IYhSubContLeaderInfoDAL _da = new YhSubContLeaderInfoDAL();

        public int Add(SubContLeaderInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public SubContLeaderInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<SubContLeaderInfo> GetItems(SubContLeaderInfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.LeaderName))
                filters.Add(new QueryParameterInfo("LeaderName", "%" + data.LeaderName + "%", DataFilterConditions.Like));
            if (data.SubContractorID != null && data.SubContractorID > 0)
                filters.Add(new QueryParameterInfo("SubContractorID", data.SubContractorID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<SubContLeaderInfo> GetPagedList(SubContLeaderInfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.LeaderName))
                        filters.Add(new QueryParameterInfo("LeaderName", "%" + data.LeaderName + "%", DataFilterConditions.Like));
                    if (data.SubContractorID != null && data.SubContractorID > 0)
                        filters.Add(new QueryParameterInfo("SubContractorID", data.SubContractorID, DataFilterConditions.Equal));

                    //if (data.SortField == "OWNER_CODE")
                    //    data.SortField = "BO.CODE";
                    //else
                    //    data.SortField = "BU." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<SubContLeaderInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<SubContLeaderInfo>("", ex);
            }
            return new PagedList<SubContLeaderInfo>();
        }

        public bool Update(SubContLeaderInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(SubContLeaderInfo model)
        {
            var queryInfo = new SubContLeaderInfoDto()
            {
                LeaderName = model.LeaderName,
                PhoneNumber = model.PhoneNumber
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
