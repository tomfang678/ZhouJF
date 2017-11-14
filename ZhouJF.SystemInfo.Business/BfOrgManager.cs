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
using YHPT.SystemInfo.Model.BfOrg;

namespace YHPT.SystemInfo.Business
{
    public class BfOrgManager : IBussiness<BfOrgInfo, QueryBfOrgDto>
    {
        private readonly IBfOrgDAL _da = new BfOrgDAL();

        public int Add(BfOrgInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfOrgInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfOrgInfo> GetItems(QueryBfOrgDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.NAME))
                filters.Add(new QueryParameterInfo("NAME", "%" + data.NAME + "%", DataFilterConditions.Like));
            if (data.OWNER_ID > 0)
                filters.Add(new QueryParameterInfo("OWNER_ID", data.OWNER_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfOrgInfo> GetPagedList(QueryBfOrgDto data)
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
                    if (data.OWNER_ID > 0)
                        filters.Add(new QueryParameterInfo("OWNER_ID", data.OWNER_ID, DataFilterConditions.Equal));

                    if (data.SortField == "OWNER_CODE")
                        data.SortField = "B.CODE";
                    else
                        data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfOrgInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfOrgInfo>("", ex);
            }
            return new PagedList<BfOrgInfo>();
        }

        public bool Update(BfOrgInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfOrgInfo model)
        {
            var queryInfo = new QueryBfOrgDto()
            {
                CODE = model.CODE
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public List<BfOrgInfo> GetAllOrgByCombin()
        {
            var orgList = GetItems(new QueryBfOrgDto());

            var result = GetChildOrgs(0, orgList);

            return result;
        }

        private List<BfOrgInfo> GetChildOrgs(int parentId, List<BfOrgInfo> orgList)
        {
            if (parentId < 0 || orgList == null || orgList.Count < 1)
                return null;
            var result = new List<BfOrgInfo>();
            result.AddRange(orgList.FindAll(a => a.OWNER_ID == parentId));
            if (result.Count > 0)
            {
                foreach (var org in result)
                {
                    org.CHILDS = GetChildOrgs(org.ID, orgList);
                }
            }
            return result;
        }
    }
}
