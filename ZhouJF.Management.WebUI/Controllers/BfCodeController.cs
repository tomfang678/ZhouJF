using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MB.Framework.Common.Caching;
using MB.Framework.Common.Enums;
using MB.Framework.Common.Logging;
using MB.Framework.Common.Model;
using Newtonsoft.Json;
using ServiceStack;
using YHPT.Management.WebUI.CommonLogic;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfCode;

namespace YHPT.Management.WebUI.Controllers
{
    public class BfCodeController : BaseController
    {
        //
        // GET: /BfCode/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryBfCode(JQueryDataTablesModel jQueryDataTablesModel, QueryBfCodeDto query)
        {
            var queryParam = new QueryBfCodeDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                CODE = query.CODE,
                DESCRIPTION = query.DESCRIPTION,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new BfCodeInfoManager()).GetPagedList(queryParam);
            return DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

        /// <summary>
        /// 查询明细信息
        /// </summary>
        /// <param name="mainId">主表关键字段</param>
        /// <returns>明细页面</returns>
        [HttpPost]
        public ActionResult GetBfCodeDtl(string mainId)
        {
            var queryParam = new QueryBfCodeDetailDto();
            queryParam.PageIndex = 0;
            queryParam.PageSize = int.MaxValue;
            queryParam.MainId = Convert.ToInt32(mainId);
            queryParam.SortField = "Code";
            var pageList = (new BfCodeDetailInfoManager()).GetPagedList(queryParam);
            return PartialView("Detail", pageList.Data);
        }

        [MenuItem("~/BfCode/Index", AuthorizeKey.Update)]
        public ActionResult Edit(int id)
        {
            var entity = (new BfCodeInfoManager()).GetItemByKey(id);
            return View(entity);
        }

        [HttpPost]
        [MenuItem("~/BfCode/Index", AuthorizeKey.Update)]
        public JsonResult Edit(BfCodeInfo model, string pdCodes, string pdNames, string sortNos, string Ids, string gridStage)
        {
            model.LAST_MODIFIED_DATE = DateTime.Now;
            //添加参数明细
            if (!string.IsNullOrEmpty(pdCodes))
            {
                var pdCodeList = pdCodes.Split(',');
                var pdNamesList = pdNames.Split(',');
                var sortNoList = sortNos.Split(',');
                var idList = Ids.Split(',');
                var dtlList = pdCodeList.Select((t, i) => new BfCodeDetailInfo
                {
                    CODE = t,
                    NAME = pdNamesList[i],
                    SEQ_NUM = sortNoList[i],
                    LAST_MODIFIED_DATE = DateTime.Now,
                    ID = int.Parse(idList[i])
                }).ToList();
                model.BfCodeDtlInfoList = dtlList;
            }

            var result = (new BfCodeInfoManager()).Update(model);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
        }

        [HttpPost]
        [MenuItem("~/BfCode/Index", AuthorizeKey.Delete)]
        public JsonResult Delete(int id)
        {
            var result = (new BfCodeInfoManager()).Delete(id);
            if (result)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });
        }

        [MenuItem("~/BfCode/Index", AuthorizeKey.Add)]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="id">明细主键</param>
        /// <returns></returns>
        public bool DeleteDtlById(string id)
        {
            var data = (new BfCodeDetailInfoManager()).Delete(int.Parse(id));
            if (data)
                return true;
            return false;
        }

        [HttpPost]
        [MenuItem("~/BfCode/Index", AuthorizeKey.Add)]
        public JsonResult Add(BfCodeInfo model, string pdCodes, string pdNames, string sortNos)
        {
            if (string.IsNullOrEmpty(model.CODE) || string.IsNullOrEmpty(model.DESCRIPTION))
            {
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)ResponseIntValue.Fail, Message = "请输入必填字段" });
            }
            model.LAST_MODIFIED_DATE = DateTime.Now;

            //添加参数明细
            if (!string.IsNullOrEmpty(pdCodes))
            {
                var pdCodeList = pdCodes.Split(',');
                var pdNamesList = pdNames.Split(',');
                var sortNoList = sortNos.Split(',');
                var dtlList = pdCodeList.Select((t, i) => new BfCodeDetailInfo
                {
                    CODE = t,
                    NAME = pdNamesList[i],
                    SEQ_NUM = sortNoList[i],
                    LAST_MODIFIED_DATE = DateTime.Now
                }).ToList();
                model.BfCodeDtlInfoList = dtlList;
            }

            var result = (new BfCodeInfoManager()).Add(model);
            if (result > 0)
                return Json(new ResponseMessage() { IsSuccess = true });
            return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "新增失败" });
        }

        [MenuItem("~/BfCode/Index")]
        public void Export(QueryBfCodeDto queryParams)
        {
            try
            {
                var bfCodeList = (new BfCodeInfoManager()).GetItems(queryParams);
                var bfCodeDtlList = (new BfCodeDetailInfoManager()).SelectBfCodeDetailByBfCodeIds(bfCodeList);
                var dataTable = new DataTable();
                string fileName = "系统参数.xls";
                LogHelper.Info(this.GetType(), "生成Excel，请稍后...");


                #region 添加列

                dataTable.Columns.Add("CODE");
                dataTable.Columns.Add("DESCRIPTION");
                dataTable.Columns.Add("DETAILCODE");
                dataTable.Columns.Add("DETAILNAME");
                dataTable.Columns.Add("SEQ_NUM");
                #endregion

                #region 添加行

                foreach (var dtl in bfCodeDtlList)
                {
                    var bfCode = bfCodeList.Find(a => a.ID == dtl.BF_CODE_ID);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["CODE"] = bfCode.CODE;
                    dataRow["DESCRIPTION"] = bfCode.DESCRIPTION;
                    dataRow["DETAILCODE"] = dtl.CODE;
                    dataRow["DETAILNAME"] = dtl.NAME;
                    dataRow["SEQ_NUM"] = dtl.SEQ_NUM;
                    dataTable.Rows.Add(dataRow);
                }

                #endregion

                #region 设置列名

                dataTable.Columns["CODE"].ColumnName = "编码";
                dataTable.Columns["DESCRIPTION"].ColumnName = "描述";
                dataTable.Columns["DETAILCODE"].ColumnName = "明细编码";
                dataTable.Columns["DETAILNAME"].ColumnName = "明细名称";
                dataTable.Columns["SEQ_NUM"].ColumnName = "明细排序号";
                #endregion

                
                System.Web.HttpContext.Current.Response.Charset = "utf-8";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                if (System.Web.HttpContext.Current.Request.Browser.Browser != "Firefox")
                    fileName = System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                FileNPOI.Export(System.Web.HttpContext.Current, fileName, dataTable);
            }
            catch (Exception ex)
            {
                LogHelper.Error(this.GetType(), "", ex);
            }
        }

        [HttpPost]
        public JsonResult CheckCode(BfCodeInfo model)
        {
            return Json((new BfCodeInfoManager()).CheckCode(model));
        }

        [MenuItem("~/BfCode/Index")]
        public void ImportTemplate()
        {
            try
            {
                LogHelper.Info(this.GetType(), "生成Excel，请稍后...");

                #region 返回DataTable
                var dataTable = GetImportDT();
                var dataRow = dataTable.NewRow();
                dataTable.Rows.Add(dataRow);
                #endregion

                string fileName = "系统参数导入模版.xls";
                System.Web.HttpContext.Current.Response.Charset = "utf-8";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                if (System.Web.HttpContext.Current.Request.Browser.Browser != "Firefox")
                    fileName = System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8);
                FileNPOI.Export(System.Web.HttpContext.Current, fileName, dataTable);
            }
            catch (Exception ex)
            {
                LogHelper.Error(this.GetType(), "", ex);
            }
        }

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <returns></returns>
        [MenuItem("~/BfCode/Index", "IMPORT")]
        public string Import()
        {
            var result = new CheckEntityResult("操作成功");
            List<ImportBfCodeDto> data = null;
            try
            {
                EnsureFolderOnserver("FileUpload");
                var dataTable = CSVFile.Import(Request);
                var list = new List<ImportBfCodeDto>();
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    var dataRow = dataTable.Rows[i];
                    var info = new ImportBfCodeDto
                    {
                        ErrorMsg = "",
                        CODE = dataRow["编码"] == DBNull.Value ? "" : dataRow["编码"].ToString().Trim(),
                        DESCRIPTION = dataRow["描述"] == DBNull.Value ? "" : dataRow["描述"].ToString().Trim(),
                        DETAILCODE = dataRow["明细编码"] == DBNull.Value ? "" : dataRow["明细编码"].ToString().Trim(),
                        DETAILNAME = dataRow["明细名称"] == DBNull.Value ? "" : dataRow["明细名称"].ToString().Trim(),
                        SEQ_NUM = dataRow["明细排序号"] == DBNull.Value ? "" : dataRow["明细排序号"].ToString().Trim()
                    };
                    list.Add(info);
                }
                var flag = (new BfCodeInfoManager()).BatchInsert(list, ref data);
                if (!flag)
                {
                    //todo:将来扩充分布式服务器，现有先发多服务器有问题
                    ICache cacheManager = new CacheManager();
                    var errorKey = UserSession.Current.UserID + "_BfCodeImport";
                    if (cacheManager.Exist(errorKey))
                        cacheManager.Remove(errorKey);
                    cacheManager.Add(errorKey, data, DateTime.Now.AddMinutes(10));
                    result.Success = false;
                    result.ErrorKey = errorKey;
                    result.Msg = "不成功";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(this.GetType(), "", ex);
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 导出异常数据
        /// </summary>
        /// <param name="cacheKey"></param>
        public void GetImportError(string cacheKey)
        {
            //todo:将来扩充分布式服务器，现有先发多服务器有问题
            ICache cacheManager = new CacheManager();
            var data = cacheManager.Get<List<ImportBfCodeDto>>(cacheKey);
            cacheManager.Remove(cacheKey);
            //导出错误数据
            this.ExportImportErrorResult(data);
        }

        /// <summary>
        /// 导出错误
        /// </summary>
        /// <param name="list"></param>
        public void ExportImportErrorResult(List<ImportBfCodeDto> list)
        {
            var dataTable = GetImportDT();
            dataTable.Columns.Add("ERRORMSG");
            dataTable.Columns["ERRORMSG"].ColumnName = "错误";

            #region 添加行
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["编码"] = item.CODE;
                    dataRow["描述"] = item.DESCRIPTION;
                    dataRow["明细编码"] = item.DETAILCODE;
                    dataRow["明细名称"] = item.DETAILNAME;
                    dataRow["明细排序号"] = item.SEQ_NUM;
                    dataRow["错误"] = item.ErrorMsg;
                    dataTable.Rows.Add(dataRow);
                }
            }
            #endregion

            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            string fileName = "错误列表.xls";
            if (System.Web.HttpContext.Current.Request.Browser.Browser != "Firefox")
                fileName = HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            FileNPOI.Export(System.Web.HttpContext.Current, fileName, dataTable);
        }
        #endregion

        private DataTable GetImportDT()
        {
            var dataTable = new DataTable();
            #region 添加列

            dataTable.Columns.Add("CODE");
            dataTable.Columns.Add("DESCRIPTION");
            dataTable.Columns.Add("DETAILCODE");
            dataTable.Columns.Add("DETAILNAME");
            dataTable.Columns.Add("SEQ_NUM");
            #endregion

            #region 设置列名

            dataTable.Columns["CODE"].ColumnName = "编码";
            dataTable.Columns["DESCRIPTION"].ColumnName = "描述";
            dataTable.Columns["DETAILCODE"].ColumnName = "明细编码";
            dataTable.Columns["DETAILNAME"].ColumnName = "明细名称";
            dataTable.Columns["SEQ_NUM"].ColumnName = "明细排序号";
            #endregion

            return dataTable;
        }
    }
}
