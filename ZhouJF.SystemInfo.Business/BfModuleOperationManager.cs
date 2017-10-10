using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.Business
{
    public class BfModuleOperationManager : IBussiness<BfModuleOperationInfo, QueryBfModuleOperationDto>
    {
        private readonly IBfModuleOperationDAL _da = new BfModuleOperationDAL();

        public int Add(BfModuleOperationInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfModuleOperationInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfModuleOperationInfo> GetItems(QueryBfModuleOperationDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (data.BF_MODULE_ID != null)
                filters.Add(new QueryParameterInfo("BF_MODULE_ID", data.BF_MODULE_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfModuleOperationInfo> GetPagedList(QueryBfModuleOperationDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.CODE))
                        filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
                    if (data.BF_MODULE_ID != null)
                        filters.Add(new QueryParameterInfo("BF_MODULE_ID", data.BF_MODULE_ID, DataFilterConditions.Equal));

                    data.SortField = data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfModuleOperationInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfModuleOperationInfo>("", ex);
            }
            return new PagedList<BfModuleOperationInfo>();
        }

        public bool Update(BfModuleOperationInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public int DeleteByModuleID(int moduleID, DataBaseResource dbResource = DataBaseResource.Default)
        {
            return _da.DeleteByModuleID(moduleID, dbResource);
        }

        public bool CheckCode(BfModuleOperationInfo model)
        {
            var queryInfo = new QueryBfModuleOperationDto()
            {
                CODE = model.CODE,
                BF_MODULE_ID = model.BF_MODULE_ID
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public List<BfModuleOperationInfo> SelectOperationByUserID(int userID)
        {
            return _da.SelectOperationByUserID(userID);
        }

        public List<BfModuleOperationInfo> SelectOperationByModule(List<BfModuleInfo> moduleInfos)
        {
            var ids = new List<int>();
            foreach (var bfModuleInfo in moduleInfos)
            {
                ids.Add(bfModuleInfo.ID);
            }
            return _da.SelectOperationByModule(ids);
        }
    }
}
