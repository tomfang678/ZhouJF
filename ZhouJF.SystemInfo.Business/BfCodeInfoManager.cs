using System;
using System.Collections.Generic;
using System.Linq;
using MB.Framework.Common;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.Common.Utility;
using MB.Framework.RuleBase.IBussiness;
using MB.Util;
using MB.Util.Model;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.IDataAccess;
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.SystemInfo.Business
{
    public class BfCodeInfoManager : IBussiness<BfCodeInfo, QueryBfCodeDto>
    {
        private readonly IBfCodeInfoDAL _da = new BfCodeInfoDAL();

        public int Add(BfCodeInfo item)
        {
            if (CheckCode(item))
                return _da.Insert(item, DataBaseResource.Read);
            return 0;
        }

        public bool BatchInsert(List<ImportBfCodeDto> items, ref List<ImportBfCodeDto> returnDtos)
        {
            var flag = true;
            returnDtos = new List<ImportBfCodeDto>();
            //执行集合
            var execList = new List<BfCodeInfo>();
            //导入数据里所有的CODE（去重后）
            var mainCodes = new List<string>();
            var groupList = items.GroupBy(temp => temp.CODE + "|" + temp.DESCRIPTION);
            foreach (var item in items)
            {
                #region 为空判断
                if (string.IsNullOrEmpty(item.CODE))
                {
                    item.ErrorMsg = "编码不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.DESCRIPTION))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "描述不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.DETAILCODE))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "明细编码不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.DETAILNAME))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "明细名称不能为空";
                    flag = false;
                }
                if (string.IsNullOrEmpty(item.SEQ_NUM))
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "明细排序号不能为空";
                    flag = false;
                }
                #endregion

                #region 字段有效性判断

                if (!string.IsNullOrEmpty(item.SEQ_NUM))
                {
                    if (!RegexHelper.IsInt(item.SEQ_NUM))
                    {
                        if (!string.IsNullOrEmpty(item.ErrorMsg))
                            item.ErrorMsg += ";";
                        item.ErrorMsg += "明细排序号必须为数字";
                        flag = false;
                    }
                }

                #endregion

                #region 明细编号重复判断

                if (items.Count(a => a.DETAILCODE == item.DETAILCODE) > 1)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "明细编码有重复";
                    flag = false;
                }
                #endregion

                #region 分组判断

                var tmp = from a in groupList where a.Key.StartsWith(item.CODE + "|") select a.Key;
                if (!tmp.Any())
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "编码分组错误";
                    flag = false;
                }
                else if (tmp.First().Split('|')[1] != item.DESCRIPTION)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMsg))
                        item.ErrorMsg += ";";
                    item.ErrorMsg += "同一个编码的描述出现不一致";
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
                var allMainList = _da.GetItems(filters, DataBaseResource.Read) ?? new List<BfCodeInfo>();

                #endregion

                #region 获取已有细表信息

                var allDetailList = new List<BfCodeDetailInfo>();
                if (allMainList.Count > 0)
                {
                    filters = new List<QueryParameterInfo>
                    {
                        new QueryParameterInfo("BF_CODE_ID", string.Join(",", allMainList.Select(a => a.ID)),
                            DataFilterConditions.In)
                    };
                    var detailInfoDal = new BfCodeDetailInfoDAL();
                    allDetailList = detailInfoDal.GetItems(filters, DataBaseResource.Read);
                }

                #endregion

                foreach (var item in groupList)
                {
                    var code = item.Key.Split('|')[0];
                    var description = item.Key.Split('|')[1];

                    var oldDtlLst = items.Where(a => a.CODE == code);

                    var bfCodeInfo = new BfCodeInfo
                    {
                        CODE = code,
                        DESCRIPTION = description,
                        LAST_MODIFIED_DATE = DateTime.Now,
                        BfCodeDtlInfoList = oldDtlLst.Select(detail => new BfCodeDetailInfo()
                        {
                            CODE = detail.DETAILCODE,
                            NAME = detail.DETAILNAME,
                            SEQ_NUM = detail.SEQ_NUM,
                            LAST_MODIFIED_DATE = DateTime.Now
                        }).ToList()
                    };
                    var oldList = allMainList.Where(a => a.CODE == code);
                    //已有数据
                    if (oldList.Any())
                    {
                        bfCodeInfo.ID = oldList.First().ID;
                        if (bfCodeInfo.BfCodeDtlInfoList != null)
                        {
                            bfCodeInfo.BfCodeDtlInfoList.ForEach(a =>
                            {
                                a.BF_CODE_ID = bfCodeInfo.ID;
                                var tmpDetail = allDetailList.Where(p => p.BF_CODE_ID == bfCodeInfo.ID && p.CODE == a.CODE);
                                if (tmpDetail.Any())
                                    a.ID = tmpDetail.First().ID;
                            });
                        }
                    }
                    execList.Add(bfCodeInfo);
                }
                _da.BatchImport(execList);
            }
            return flag;
        }

        public bool Delete(object key)
        {
            (new BfCodeDetailInfoManager()).DeleteByBfCodeId(Convert.ToInt32(key));
            return _da.Delete(key, DataBaseResource.Write);
        }

        public BfCodeInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }

        public List<BfCodeInfo> GetItems(QueryBfCodeDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            if (!string.IsNullOrEmpty(data.CODE))
                filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
            if (!string.IsNullOrEmpty(data.DESCRIPTION))
                filters.Add(new QueryParameterInfo("DESCRIPTION", "%" + data.DESCRIPTION + "%", DataFilterConditions.Like));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<BfCodeInfo> GetPagedList(QueryBfCodeDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    if (!string.IsNullOrEmpty(data.CODE))
                        filters.Add(new QueryParameterInfo("CODE", data.CODE, DataFilterConditions.Equal));
                    if (!string.IsNullOrEmpty(data.DESCRIPTION))
                        filters.Add(new QueryParameterInfo("DESCRIPTION", "%" + data.DESCRIPTION + "%", DataFilterConditions.Like));

                    data.SortField = "A." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<BfCodeInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<BfCodeInfo>("", ex);
            }
            return new PagedList<BfCodeInfo>();
        }

        public bool Update(BfCodeInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(BfCodeInfo model)
        {
            var queryInfo = new QueryBfCodeDto()
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

        public BfCodeDetailInfo GetBfCodeByModuleId(String moduleId)
        {
            return _da.GetBfCodeByModuleId(moduleId);
        }
    }
}
