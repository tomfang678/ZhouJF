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
using YHPT.SystemInfo.Model.Subcontractor;
using YHPT.SystemInfo.DAL;
using YHPT.SystemInfo.Model.YhManager;
using SmartFast.BaseFrame.Utility;

namespace YHPT.SystemInfo.Business
{
    public class YhImgInfoManager : IBussiness<ImgInfo, ImgInfoDto>
    {
        private readonly IYhImgInfoDAL _da = new YhImgInfoDAL();

        public int Add(ImgInfo item)
        {
            if (!string.IsNullOrEmpty(item.imgUrl))
            {
                item.thembUrl = ImageUtil.GetThumbnail(item.imgUrl);
            }
            //if (CheckCode(item))
            return _da.Insert(item, DataBaseResource.Read);
            //return 0;
        }

        public bool Delete(object key)
        {
            return _da.Delete(key, DataBaseResource.Write);
        }

        public ImgInfo GetItemByKey(object key)
        {
            var entity = _da.GetItemByKey(key, DataBaseResource.Read);
            return entity;
        }
        public List<ImgInfo> GetImgByModule(string module, int key)
        {
            return _da.GetImgByModuleId(module, key);
        }
        public List<ImgInfo> GetImgByRoadId(int RoadId)
        {
            return _da.GetImgByRoadId(RoadId);
        }

        public List<ImgInfo> GetItems(ImgInfoDto data, List<QueryParameterInfo> queryParameterInfos = null)
        {
            var filters = new List<QueryParameterInfo>();
            //if (!string.IsNullOrEmpty(data.SubContractorBoss))
            //    filters.Add(new QueryParameterInfo("SubContractorBoss", data.SubContractorBoss, DataFilterConditions.Equal));
            //if (!string.IsNullOrEmpty(data.SubContractorCode))
            //    filters.Add(new QueryParameterInfo("SubContractorCode", "%" + data.SubContractorCode + "%", DataFilterConditions.Like));
            //if (!string.IsNullOrEmpty(data.SubContractorCrop))
            //    filters.Add(new QueryParameterInfo("SubContractorCrop", data.SubContractorCrop, DataFilterConditions.Equal));
            if (queryParameterInfos != null)
            {
                filters.AddRange(queryParameterInfos);
            }
            var result = _da.GetItems(filters, DataBaseResource.Read);
            return result;
        }

        public PagedList<ImgInfo> GetPagedList(ImgInfoDto data)
        {
            try
            {
                if (data != null)
                {
                    var filters = new List<QueryParameterInfo>();
                    //if (!string.IsNullOrEmpty(data.SubContractorBoss))
                    //    filters.Add(new QueryParameterInfo("SubContractorBoss", data.SubContractorBoss, DataFilterConditions.Equal));
                    //if (!string.IsNullOrEmpty(data.SubContractorCode))
                    //    filters.Add(new QueryParameterInfo("SubContractorCode", "%" + data.SubContractorCode + "%", DataFilterConditions.Like));
                    //if (!string.IsNullOrEmpty(data.SubContractorCrop))
                    //    filters.Add(new QueryParameterInfo("SubContractorCrop", data.SubContractorCrop, DataFilterConditions.Equal));

                    //if (data.SortField == "OWNER_CODE")
                    //    data.SortField = "BO.CODE";
                    //else
                    //    data.SortField = "BU." + data.SortField;

                    var result = _da.GetPagedList(filters, data.PageIndex, data.PageSize,
                        data.SortField, data.SortDirection, DataBaseResource.Read);
                    return result;
                }
                else
                {
                    LogHelper.Info<ImgInfo>("接收不成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<ImgInfo>("", ex);
            }
            return new PagedList<ImgInfo>();
        }

        public bool Update(ImgInfo item)
        {
            if (CheckCode(item))
                return _da.Update(item, DataBaseResource.Write);
            return false;
        }

        public bool CheckCode(ImgInfo model)
        {
            var queryInfo = new ImgInfoDto()
            {
                imgModuleId = model.imgModuleId,
                imgType = model.imgType
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
