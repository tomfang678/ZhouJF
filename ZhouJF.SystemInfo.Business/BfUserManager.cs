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

namespace YHPT.SystemInfo.Business
{
    public class BfUserManager : IBussiness<BfUserInfo, QueryBfUserDto>
    {
        private readonly IBfUserDAL _da = new BfUserInfoDAL();

        public int Add(BfUserInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfUserInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfUserInfo> GetItems(QueryBfUserDto data, List<QueryParameterInfo> queryParameterInfos = null)
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

        public PagedList<BfUserInfo> GetPagedList(QueryBfUserDto data)
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
                        data.SortField = "BO.CODE";
                    else
                        data.SortField = "BU." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfUserInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfUserInfo>("", ex);
            }
            return new PagedList<BfUserInfo>();
        }

        public bool Update(BfUserInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfUserInfo model)
        {
            var queryInfo = new QueryBfUserDto()
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

        public BfUserInfo GetItemByCode(string code)
        {
            var dto = new QueryBfUserDto();
            dto.CODE = code;
            var list = GetItems(dto);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public bool ChangePwd(BfUserInfo bfUser)
        {
            return _da.ChangePwd(bfUser);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public LoginValidateInfo<LoginUserInfo> GetSysLoginUserData(string code, string pwd)
        {
            if (string.IsNullOrEmpty(pwd))
                pwd = "";
            var loginValidateInfo = new LoginValidateInfo<LoginUserInfo>();
            var queryInfo = new QueryBfUserDto { CODE = code.ToUpper() };
            var filters = new List<QueryParameterInfo>
            {
                new QueryParameterInfo("EXPIRE_TIME", DateTime.Now, DataFilterConditions.GreaterOrEqual)

            };
            var result = GetItems(queryInfo, filters);
            if (result == null || result.Count < 1)
                loginValidateInfo.Status = UserValidateStatus.UserNotExist;
            else
            {
                var entity = result[0];
                if (string.Compare(entity.PASSWORD, pwd) != 0)
                    loginValidateInfo.Status = UserValidateStatus.PasswordError;
                else
                {
                    loginValidateInfo.Status = UserValidateStatus.Pass;
                    var userTokenManager = new UserTokenManager();
                    var userToken = userTokenManager.GetItemById(entity.ID);
                    userTokenManager.ExtendExpireTime(userToken);
                    var loginUser = new LoginUserInfo
                    {
                        ID = entity.ID,
                        CODE = entity.CODE,
                        NAME = entity.NAME,
                        ACCESSTOKEN = userToken.TOKEN
                    };
                    loginValidateInfo.Result = loginUser;
                }
            }
            return loginValidateInfo;
        }
    }
}
