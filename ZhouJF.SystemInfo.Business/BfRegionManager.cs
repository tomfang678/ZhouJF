using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YHPT.SystemInfo.DAL.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfRegion;

namespace YHPT.SystemInfo.Business
{
    public class BfRegionManager : IBussiness<BfRegionInfo, QueryBfRegionDto>
    {
        private readonly IBfRegionInfoDAL _da = new BfRegionInfoDAL();

        public int Add(BfRegionInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool BatchInsert(List<ImportBfRegionDto> items, ref List<ImportBfRegionDto> returnDtos)
        {
            var flag = true;
            returnDtos = new List<ImportBfRegionDto>();
            //执行集合
            var execList = new List<BfRegionInfo>();
            //导入数据里所有的CODE（去重后）
            var mainCodes = new List<string>();

            foreach (var item in items)
            {
                #region 为空判断
                if (string.IsNullOrEmpty(item.CODE))
                {
                    item.ErrorMsg = "编码不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.NAME))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "名称不能为空";
                    flag = false;
                }
                if (item.PARENT_ID == null)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "父层级编码不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.REGION_LEVEL))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "层级不能为空";
                    flag = false;
                }
                if (item.SQU_NUM == null)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "序列不能为空";
                    flag = false;
                }
                #endregion

                #region 明细编号重复判断

                if (items.Count(a => a.CODE == item.CODE) > 1)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "编码有重复";
                    flag = false;
                }
                #endregion

                if (!mainCodes.Contains(item.CODE))
                    mainCodes.Add(item.CODE);

                returnDtos.Add(item);
            }
            if (flag)
            {
                #region 获取已有主表信息
                var filters = new List<QueryParameterInfo>
                {
                    new QueryParameterInfo("CODE", string.Join(",", mainCodes), DataFilterConditions.In)
                };
                var allMainList = _da.GetItems(filters, DataBaseResource.Read) ?? new List<BfRegionInfo>();

                #endregion

                foreach (var item in items)
                {

                    var bfRegionInfo = new BfRegionInfo
                    {
                        CODE = item.CODE,
                        NAME = item.NAME,
                        REGION_LEVEL = item.REGION_LEVEL,
                        REMARK = item.REMARK,
                        SQU_NUM = item.SQU_NUM,
                        LAST_MODIFIED_DATE = DateTime.Now
                    };
                    var oldList = allMainList.Find(a => a.CODE == item.CODE);
                    //已有数据
                    if (oldList!=null)
                    {
                        bfRegionInfo.ID = oldList.ID;
                        
                    }
                    execList.Add(bfRegionInfo);
                }
                _da.BatchImport(execList);
            }
            return flag;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfRegionInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfRegionInfo> GetItems(QueryBfRegionDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.NAME))
                filters.Add(new QueryParameterInfo("NAME", "%" + data.NAME + "%", DataFilterConditions.Like));
            if (!string.IsNullOrEmpty(data.REGION_LEVEL))
                filters.Add(new QueryParameterInfo("REGION_LEVEL", data.REGION_LEVEL, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfRegionInfo> GetPagedList(QueryBfRegionDto data)
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
                    if (!string.IsNullOrEmpty(data.REGION_LEVEL))
                        filters.Add(new QueryParameterInfo("REGION_LEVEL", data.REGION_LEVEL, DataFilterConditions.Equal));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfRegionInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfRegionInfo>("", ex);
            }
            return new PagedList<BfRegionInfo>();
        }

        public bool Update(BfRegionInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfRegionInfo model)
        {
            var queryInfo = new QueryBfRegionDto()
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
    }
}
