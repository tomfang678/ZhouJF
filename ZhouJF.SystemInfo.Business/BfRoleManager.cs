using System;
using System.Collections.Generic;
using System.Linq;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfModule;
using YHPT.SystemInfo.Model.BfRole;

namespace YHPT.SystemInfo.Business
{
    public class BfRoleManager : IBussiness<BfRoleInfo, QueryBfRoleDto>
    {
        private readonly IBfRoleDAL _da = new BfRoleDAL();

        public int Add(BfRoleInfo item)
        {
            if (CheckCode(item))
            {
                int result = _da.Insert(item, DataBaseResource.Read);
                if (result > 0)
                {
                    if (item.OperationItems != null && item.OperationItems.Count > 0)
                    {
                        foreach (var operation in item.OperationItems)
                        {
                            operation.BF_ROLE_ID = result;
                        }
                        (new BfRolePermissionManager()).BatchAdd(item.OperationItems);
                    }
                    if (item.OtherOperationItems != null && item.OtherOperationItems.Count > 0)
                    {
                        foreach (var otherOperation in item.OtherOperationItems)
                        {
                            otherOperation.BF_ROLE_ID = result;
                        }
                        (new BfRoleSpecialPermissionManager()).BatchAdd(item.OtherOperationItems);
                    }
                    return result;
                }

            }
            return 0;
        }

        public bool Delete(object key)
        {
            if (_da.Delete(key, DataBaseResource.Write))
            {
                (new BfRoleUserManager()).DeleteByRoleId(Convert.ToInt32(key));
                (new BfRolePermissionManager()).DeleteByRoleId(Convert.ToInt32(key));
                (new BfRoleSpecialPermissionManager()).DeleteByRoleId(Convert.ToInt32(key));
                return true;
            }
            return false;
        }

        public BfRoleInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfRoleInfo> GetItems(QueryBfRoleDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.NAME))
                filters.Add(new QueryParameterInfo("NAME", "%" + data.NAME + "%", DataFilterConditions.Like));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfRoleInfo> GetPagedList(QueryBfRoleDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.CODE))
                        filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.NAME))
                        filters.Add(new QueryParameterInfo("NAME", "%" + data.NAME + "%", DataFilterConditions.Like));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfRoleInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfRoleInfo>("", ex);
            }
            return new PagedList<BfRoleInfo>();
        }

        public bool Update(BfRoleInfo item)
        {
            if (CheckCode(item))
            {
                bool flag = _da.Update(item, DataBaseResource.Write);
                var oldRolePermissions = (new BfRolePermissionManager()).GetItems(new QueryBfRolePermissionDto()
                {
                    BF_ROLE_ID = item.ID
                });
                var oldRoleSpecialPersmissions =
                    (new BfRoleSpecialPermissionManager()).GetItems(new QueryBfRoleSpecialPermissionDto()
                    {
                        BF_ROLE_ID = item.ID
                    });
                (new BfRolePermissionManager()).UpdateRolePermission(oldRolePermissions,item.OperationItems);
                (new BfRoleSpecialPermissionManager()).UpdateRoleSpecialPermission(oldRoleSpecialPersmissions,item.OtherOperationItems);
                return true;
            }
            return false;
        }

        public bool CheckCode(BfRoleInfo model)
        {
            var queryInfo = new QueryBfRoleDto()
            {
                CODE = model.CODE,
                NAME = model.NAME
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public List<BfModuleInfo> SelectAllOperation()
        {
            var bfModuleInfos = (new BfModuleManager()).SelectAllChildBfModuleInfos();
            List<BfModuleOperationInfo> bfModuleOperationInfos =
                (new BfModuleOperationManager()).SelectOperationByModule(bfModuleInfos);
            List<BfModuleOtherOperationInfo> bfModuleOtherOperationInfos =
                (new BfModuleOtherOperationManager()).SelectOtherOperationByModule(bfModuleInfos);
            foreach (var bfModuleInfo in bfModuleInfos)
            {
                bfModuleInfo.OperationItems = new List<BfModuleOperationInfo>();
                bfModuleInfo.OperationItems.AddRange(bfModuleOperationInfos.FindAll(a => a.BF_MODULE_ID == bfModuleInfo.ID).ToList());
                bfModuleInfo.OtherOperationItems = new List<BfModuleOtherOperationInfo>();
                bfModuleInfo.OtherOperationItems.AddRange(bfModuleOtherOperationInfos.FindAll(a => a.BF_MODULE_ID == bfModuleInfo.ID));
            }
            return bfModuleInfos;
        }
    }
}
