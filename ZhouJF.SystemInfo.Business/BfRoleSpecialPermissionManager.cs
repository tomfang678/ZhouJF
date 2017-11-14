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
    public class BfRoleSpecialPermissionManager : IBussiness<BfRoleSpecialPermissionInfo, QueryBfRoleSpecialPermissionDto>
    {
        private readonly IBfRoleSpecialPermissionDAL _da = new BfRoleSpecialPermissionDAL();

        public int Add(BfRoleSpecialPermissionInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfRoleSpecialPermissionInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfRoleSpecialPermissionInfo> GetItems(QueryBfRoleSpecialPermissionDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.BF_MODULE_SPECIAL_ID > 0)
                filters.Add(new QueryParameterInfo("BF_MODULE_SPECIAL_ID", data.BF_MODULE_SPECIAL_ID, DataFilterConditions.Equal));
            if (data.BF_ROLE_ID > 0)
                filters.Add(new QueryParameterInfo("BF_ROLE_ID", data.BF_ROLE_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfRoleSpecialPermissionInfo> GetPagedList(QueryBfRoleSpecialPermissionDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (data.BF_MODULE_SPECIAL_ID > 0)
                        filters.Add(new QueryParameterInfo("BF_MODULE_SPECIAL_ID", data.BF_MODULE_SPECIAL_ID, DataFilterConditions.Equal));
                    if (data.BF_ROLE_ID > 0)
                        filters.Add(new QueryParameterInfo("BF_ROLE_ID", data.BF_ROLE_ID, DataFilterConditions.Equal));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfRoleSpecialPermissionInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfRoleSpecialPermissionInfo>("", ex);
            }
            return new PagedList<BfRoleSpecialPermissionInfo>();
        }

        public bool Update(BfRoleSpecialPermissionInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfRoleSpecialPermissionInfo model)
        {
            var queryInfo = new QueryBfRoleSpecialPermissionDto()
            {
                BF_ROLE_ID = model.BF_ROLE_ID,
                BF_MODULE_SPECIAL_ID = model.BF_MODULE_SPECIAL_ID
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public void BatchAdd(List<BfRoleSpecialPermissionInfo> roleSpecialPermissionInfos)
        {
            _da.BatchAdd(roleSpecialPermissionInfos);
        }

        public int DeleteByRoleId(int roleId)
        {
            return _da.DeleteByRoleId(roleId);
        }

        public void UpdateRoleSpecialPermission(List<BfRoleSpecialPermissionInfo> oldList, List<BfRoleSpecialPermissionInfo> newList)
        {
            if (newList != null)
            {
                for (int i = newList.Count - 1; i >= 0; i--)
                {
                    var oldRoleSpecialPermission = oldList.Find(a => a.BF_MODULE_SPECIAL_ID == newList[i].BF_MODULE_SPECIAL_ID
                        && a.BF_ROLE_ID == newList[i].BF_ROLE_ID);
                    if (oldRoleSpecialPermission != null)
                    {
                        oldList.Remove(oldRoleSpecialPermission);
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
