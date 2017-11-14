using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRole;
using YHPT.SystemInfo.Model.BfRoleUser;

namespace YHPT.SystemInfo.Business
{
    public class BfRoleUserManager : IBussiness<BfRoleUserInfo, BfRoleUserInfo>
    {
        private readonly IBfRoleUserDAL _da = new BfRoleUserDAL();

        public PagedList<BfRoleUserInfo> GetPagedList(BfRoleUserInfo data)
        {
            throw new NotImplementedException();
        }

        public BfRoleUserInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfRoleUserInfo> GetItems(BfRoleUserInfo data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (data.BF_ROLE_ID != null)
                filters.Add(new QueryParameterInfo("BF_ROLE_ID", data.BF_ROLE_ID, DataFilterConditions.Equal));
            if (data.BF_USER_ID != null)
                filters.Add(new QueryParameterInfo("BF_USER_ID", data.BF_USER_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public int Add(BfRoleUserInfo item)
        {
            return _da.Insert(item, DataBaseResource.Read);
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public bool Update(BfRoleUserInfo item)
        {
            return _da.Update(item, DataBaseResource.Write);
        }

        public List<BfRoleInfo> GetRoleByUser(int userId)
        {
            return _da.GetRoleByUser(userId);
        }

        public void BatchAdd(List<BfRoleUserInfo> roleInfoList)
        {
            _da.BatchAdd(roleInfoList);
        }

        public int DeleteByUserId(int UserId)
        {
            return _da.DeleteByUserId(UserId);
        }

        public void UpdateRoleUser(List<BfRoleUserInfo> oldList, List<BfRoleUserInfo> newList)
        {
            if (newList != null)
            {
                for (int i = newList.Count - 1; i >= 0; i--)
                {
                    var oldRole = oldList.Find(a => a.BF_ROLE_ID == newList[i].BF_ROLE_ID && a.BF_USER_ID == newList[i].BF_USER_ID);
                    if (oldRole != null)
                    {
                        oldList.Remove(oldRole);
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

        public int DeleteByRoleId(int roleId)
        {
            return _da.DeleteByRoleId(roleId);
        }
    }
}
