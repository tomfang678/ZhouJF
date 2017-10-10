using System;
using System.Collections.Generic;
using System.Configuration;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Model;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfUser;

namespace YHPT.SystemInfo.Business
{
    public class UserTokenManager
    {
        private readonly IUserTokenDAL _da = new UserTokenDAL();
        private readonly string TokenExprire = ConfigurationManager.AppSettings["TokenExprire"];

        /// <summary>
        /// 获取TOKEN
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserTokenInfo GetItemById(int userID,bool IsExpireCreate = true)
        {
            var userToken = new UserTokenInfo();
            //配置时间没有，直接创建TOKEN
            if (string.IsNullOrEmpty(TokenExprire))
            {
                userToken = CreateUserToken(userID);
                Add(userToken);
                return userToken;
            }
            var filters = new List<QueryParameterInfo>
            {
                new QueryParameterInfo("USERID", userID, DataFilterConditions.Equal)
            };

            var list = _da.GetItems(filters, DataBaseResource.Read);
            //当前用户没有TOKEN记录，创建TOKEN
            if (list == null || list.Count < 1)
            {
                userToken = CreateUserToken(userID);
                Add(userToken);
                return userToken;
            }
            //TOKEN超过有效期将删除重新创建TOKEN
            if (IsExpireCreate)
            {
                if (list[0].CREATE_TIME.AddMinutes(Convert.ToInt32(TokenExprire)) < DateTime.Now)
                {
                    userToken = CreateUserToken(userID);
                    Add(userToken);
                    return userToken;
                }
            }
            //在时间范围的TOKEN直接返回
            return list[0];
        }

        /// <summary>
        /// 验证TOKEN
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public ValidateStatus ValidatorToken(int userID, string token)
        {
            //根据用户获取TOKEN
            var entity = GetItemById(userID,false);
            if (entity.TOKEN.Equals(token))
            {
                if (entity.CREATE_TIME.AddMinutes(Convert.ToInt32(TokenExprire)) >= DateTime.Now)
                {
                    return ValidateStatus.Pass;
                }
                return ValidateStatus.TokenExpire;
            }
            return ValidateStatus.InvalidToken;
        }

        private int Add(UserTokenInfo item)
        {
            _da.Delete(item.USERID, DataBaseResource.Write);
            return _da.Insert(item);
        }

        private UserTokenInfo CreateUserToken(int userID)
        {
            return new UserTokenInfo
            {
                USERID = userID,
                TOKEN = Guid.NewGuid().ToString(),
                CREATE_TIME = DateTime.Now
            };
        }

        /// <summary>
        /// 延长TOKEN有效期
        /// </summary>
        public void ExtendExpireTime(UserTokenInfo userToken)
        {
            _da.Update(userToken, DataBaseResource.Write);
        }
    }
}
