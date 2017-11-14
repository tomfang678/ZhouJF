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
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.Business
{
    public class BfRolePermissionManager : IBussiness<BfRolePermissionInfo, QueryBfRolePermissionDto>
    {
        private readonly IBfRolePermissionDAL _da = new BfRolePermissionDAL();

        public int Add(BfRolePermissionInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfRolePermissionInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfRolePermissionInfo> GetItems(QueryBfRolePermissionDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.BF_MODULE_OPERATION_ID>0)
                filters.Add(new QueryParameterInfo("BF_MODULE_OPERATION_ID", data.BF_MODULE_OPERATION_ID, DataFilterConditions.Equal));
            if (data.BF_ROLE_ID > 0)
                filters.Add(new QueryParameterInfo("BF_ROLE_ID", data.BF_ROLE_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfRolePermissionInfo> GetPagedList(QueryBfRolePermissionDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (data.BF_MODULE_OPERATION_ID > 0)
                        filters.Add(new QueryParameterInfo("BF_MODULE_OPERATION_ID", data.BF_MODULE_OPERATION_ID, DataFilterConditions.Equal));
                    if (data.BF_ROLE_ID > 0)
                        filters.Add(new QueryParameterInfo("BF_ROLE_ID", data.BF_ROLE_ID, DataFilterConditions.Equal));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfRolePermissionInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfRolePermissionInfo>("", ex);
            }
            return new PagedList<BfRolePermissionInfo>();
        }

        public bool Update(BfRolePermissionInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfRolePermissionInfo model)
        {
            var queryInfo = new QueryBfRolePermissionDto()
            {
                BF_ROLE_ID = model.BF_ROLE_ID,
                BF_MODULE_OPERATION_ID = model.BF_MODULE_OPERATION_ID
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public void BatchAdd(List<BfRolePermissionInfo> rolePermissionInfos)
        {
            _da.BatchAdd(rolePermissionInfos);
        }

        public int DeleteByRoleId(int roleId)
        {
            return _da.DeleteByRoleId(roleId);
        }

        public void UpdateRolePermission(List<BfRolePermissionInfo> oldList, List<BfRolePermissionInfo> newList)
        {
            if (newList != null)
            {
                for (int i = newList.Count - 1; i >= 0; i--)
                {
                    var oldRolePermission = oldList.Find(a => a.BF_MODULE_OPERATION_ID == newList[i].BF_MODULE_OPERATION_ID 
                        && a.BF_ROLE_ID == newList[i].BF_ROLE_ID);
                    if (oldRolePermission != null)
                    {
                        oldList.Remove(oldRolePermission);
                        newList.Remove(newList[i]);
                    }
                }
            }
            foreach (var oldRole in oldList)
            {
                _da.Delete(oldRole.ID);
            }
            _da.BatchAdd(newList);
        }
    }
}
