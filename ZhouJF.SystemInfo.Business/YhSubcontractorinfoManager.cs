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

namespace YHPT.SystemInfo.Business
{
    public class YhSubcontractorinfoManager : IBussiness<SubContractorInfo, SubContractorDto>
    {
        private readonly IYhSubcontractorinfoDAL _da = new YhSubcontractorinfoDAL();

        public int Add(SubContractorInfo item)
        {
            //if (CheckCode(item))
            return _da.Insert(item, DataBaseResource.Read);
            //return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public SubContractorInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<SubContractorInfo> GetItems(SubContractorDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.SubContractorBoss))
                filters.Add(new QueryParameterInfo("SubContractorBoss", data.SubContractorBoss, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.SubContractorCode))
                filters.Add(new QueryParameterInfo("SubContractorCode", "%" + data.SubContractorCode + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.SubContractorCrop))
                filters.Add(new QueryParameterInfo("SubContractorCrop", data.SubContractorCrop, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<SubContractorInfo> GetPagedList(SubContractorDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.SubContractorBoss))
                        filters.Add(new QueryParameterInfo("SubContractorBoss", data.SubContractorBoss, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.SubContractorCode))
                        filters.Add(new QueryParameterInfo("SubContractorCode", "%" + data.SubContractorCode + "%", DataFilterConditions.Like));
                    if (!string.IsNullOrEmpty(data.SubContractorCrop))
                        filters.Add(new QueryParameterInfo("SubContractorCrop", data.SubContractorCrop, DataFilterConditions.Equal));

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
                    LogHelper.Info<SubContractorInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<SubContractorInfo>("", ex);
            }
            return new PagedList<SubContractorInfo>();
        }

        public bool Update(SubContractorInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(SubContractorInfo model)
        {
            var queryInfo = new SubContractorDto()
            {
                SubContractorCode = model.SubContractorCode
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
