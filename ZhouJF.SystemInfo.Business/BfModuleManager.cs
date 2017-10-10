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
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.SystemInfo.Business
{
    public class BfModuleManager : IBussiness<BfModuleInfo, QueryBfModuleDto>
    {
        private readonly IBfModuleDAL _da = new BfModuleInfoDAL();
        private readonly BfModuleOperationManager _operationManager = new BfModuleOperationManager();
        private readonly BfModuleOtherOperationManager _otherOperateManager = new BfModuleOtherOperationManager();

        public int Add(BfModuleInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfModuleInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            if (entity != null)
            {
                entity.OperationItems = _operationManager.GetItems(new QueryBfModuleOperationDto()
                {
                    BF_MODULE_ID = Convert.ToInt32(key)
                });
                entity.OtherOperationItems = _otherOperateManager.GetItems(new QueryBfModuleOtherOperationDto()
                {
                    BF_MODULE_ID = Convert.ToInt32(key)
                });
            }
            return entity;
        }

        public List<BfModuleInfo> GetItems(QueryBfModuleDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.NAME))
                filters.Add(new QueryParameterInfo("NAME", "%" + data.NAME + "%", DataFilterConditions.Like));
            if(data.PARENT_ID!=null)
                filters.Add(new QueryParameterInfo("PARENT_ID", data.PARENT_ID, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfModuleInfo> GetPagedList(QueryBfModuleDto data)
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
                    if (data.PARENT_ID != null)
                        filters.Add(new QueryParameterInfo("PARENT_ID", data.PARENT_ID, DataFilterConditions.Equal));
                    
                    data.SortField = data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfModuleInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfModuleInfo>("", ex);
            }
            return new PagedList<BfModuleInfo>();
        }

        public bool Update(BfModuleInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfModuleInfo model)
        {
            var queryInfo = new QueryBfModuleDto()
            {
                CODE = model.CODE,
                PARENT_ID = model.PARENT_ID
            };
            var filters = new List<QueryParameterInfo>();
            if (model.ID > 0)
            {
                filters.Add(new QueryParameterInfo("ID", model.ID, DataFilterConditions.NotEqual));
            }
            var result = GetItems(queryInfo, filters);
            return result == null || result.Count == 0;
        }

        public bool DeleteItemAndChildrenById(int id)
        {
            //获取主键为id的数据
            var lst = new List<BfModuleInfo> {GetItemByKey(id)};
            lst.AddRange(GetItemByParentID(id));
            if (lst != null)
            {
                foreach (var info in lst)
                {
                    Delete(info.ID);
                }
            }

            //删除模块操作
            _operationManager.DeleteByModuleID(id);

            //删除其他模块操作
            _otherOperateManager.DeleteByModuleID(id);

            return true;
        }

        public bool DeleteOtherOperateByID(int id)
        {
            return _otherOperateManager.Delete(id);
        }

        public List<BfModuleInfo> GetItemByParentID(int parentId)
        {
            var retLst = new List<BfModuleInfo>();
            
            //获取父键为PARENTID的数据
            var queryBfModuleDto = new QueryBfModuleDto(){PARENT_ID = parentId};
            var childInfos = GetItems(queryBfModuleDto);
            if (childInfos != null)
            {
                retLst.AddRange(childInfos);
                foreach (var info in childInfos)
                {
                    retLst.AddRange(GetItemByParentID(info.ID));
                }
            }

            return retLst;
        }

        public List<BfModuleInfo> SelectObjectByUserID(int userID)
        {
            return _da.SelectObjectByUserID(userID);
        }

        public List<BfModuleInfo> SelectAllChildBfModuleInfos()
        {
            return _da.SelectAllChildBfModuleInfos();
        }



    }
}
