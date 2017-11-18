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
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.SystemInfo.Business
{
    /// <summary>
    /// test
    /// </summary>
    public class BfCodeDetailInfoManager : IBussiness<BfCodeDetailInfo, QueryBfCodeDetailDto>
    {
        private readonly IBfCodeDetailInfoDAL _da = new BfCodeDetailInfoDAL();
        public int Add(BfCodeDetailInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfCodeDetailInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfCodeDetailInfo> GetItems(QueryBfCodeDetailDto data, List<MB.Util.Model.QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.MainId > 0)
                filters.Add(new QueryParameterInfo("BF_CODE_ID", data.MainId, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public MB.Framework.Common.PagedList<BfCodeDetailInfo> GetPagedList(QueryBfCodeDetailDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (data.MainId > 0)
                        filters.Add(new QueryParameterInfo("BF_CODE_ID", data.MainId, DataFilterConditions.Equal));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfCodeInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfCodeInfo>("", ex);
            }
            return new PagedList<BfCodeDetailInfo>();
        }

        public bool Update(BfCodeDetailInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfCodeDetailInfo model)
        {
            var queryInfo = new QueryBfCodeDetailDto()
            {
                Code = model.CODE
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public int DeleteByBfCodeId(int bfCodeId)
        {
            return _da.DeleteByBfCodeId(bfCodeId);
        }

        public List<BfCodeDetailInfo> SelectBfCodeDetailByBfCodeIds(List<BfCodeInfo> bfCodeInfos)
        {
            var ids = new List<int>();
            foreach (var bfCodeInfo in bfCodeInfos)
            {
                ids.Add(bfCodeInfo.ID);
            }
            return _da.SelectBfCodeDetailByBfCodeIds(ids);
        }
    }
}
