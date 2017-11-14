using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MB.Framework.Common.Logging;
using MB.Framework.Common.Model;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Models;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.Management.WebUI.Controllers
{
    public class BfModuleController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获所有模块树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBfModuleJsTree()
        {
            try
            {
                var bfModuleList = (new BfModuleManager()).GetItems(new QueryBfModuleDto());
                var jsTreeList = new List<JsTreeNode>();
                jsTreeList.AddRange(bfModuleList.OrderBy(o => o.PARENT_ID).ThenBy(o => o.ORDER).Select(a => new JsTreeNode
                {
                    id = a.ID.ToString(),
                    parent = a.PARENT_ID.GetValueOrDefault() < 1 ? "#" : a.PARENT_ID.GetValueOrDefault().ToString(),
                    text = a.NAME,
                    state = new State
                    {
                        opened = true,
                        disabled = false
                    }
                }).ToList());
                var result = Json(jsTreeList);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据ID获取模块
        /// </summary>
        /// <returns></returns>
        public string GetBfModuleById(string id)
        {
            try
            {
                var bfModule = (new BfModuleManager()).GetItemByKey(id);
                string resultStr = Newtonsoft.Json.JsonConvert.SerializeObject(bfModule);
                return resultStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存模块
        /// </summary>
        /// <returns></returns> 
        public JsonResult SetBfModule(BfModuleInfo moduleInfo,
            string operations, string otherOperCodes, string otherOperNames, string otherOperIds)
        {
            try
            {
                moduleInfo.LAST_MODIFIED_TIME = DateTime.Now;
                moduleInfo.LAST_MODIFIED_USER = UserSession.Current.UserID;
                if (moduleInfo.PARENT_ID == null)
                    moduleInfo.PARENT_ID = 0;

                var tmpOperationList = operations.Split(',');
                var tmpOtherOperationCodeList = otherOperCodes.Split(',');
                var tmpOtherOperationNameList = otherOperNames.Split(',');
                var tmpOtherId = otherOperIds.Split(',');

                if (!string.IsNullOrEmpty(operations))
                {
                    var operationList = tmpOperationList.Select((t, i) => new BfModuleOperationInfo()
                    {
                        BF_MODULE_ID = moduleInfo.ID,
                        CODE = t
                    }).ToList();
                    moduleInfo.OperationItems = operationList;
                }

                if (!string.IsNullOrEmpty(otherOperIds))
                {
                    var otherOperationList = tmpOtherOperationCodeList.Select((t, i) => new BfModuleOtherOperationInfo()
                    {
                        BF_MODULE_ID = moduleInfo.ID,
                        CODE = t,
                        NAME = tmpOtherOperationNameList[i],
                        ID = Convert.ToInt32(tmpOtherId[i])
                    }).ToList();
                    moduleInfo.OtherOperationItems = otherOperationList;
                }

                if (moduleInfo.ID > 0)
                {
                    var result = (new BfModuleManager()).Update(moduleInfo);

                    //更新moduleInfo的缓存信息
                    MemuTree.Instance.RefreshModuleInfo();

                    LogHelper.Info(
                         this.GetType(),
                         string.Format("用户[{0}]更新模块Code={1}", UserSession.Current.UserCode, moduleInfo.CODE));

                    if (result)
                        return Json(new ResponseMessage() { IsSuccess = true });
                    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "修改失败" });
                }
                else
                {
                    moduleInfo.CREATE_USER = UserSession.Current.UserID;
                    moduleInfo.CREATE_DATE = System.DateTime.Now;
                    var result = (new BfModuleManager()).Add(moduleInfo);

                    //更新moduleInfo的缓存信息
                    MemuTree.Instance.RefreshModuleInfo();

                    LogHelper.Info(
                         this.GetType(),
                         string.Format("用户[{0}]添加模块Code={1}", UserSession.Current.UserCode, moduleInfo.CODE));

                    if (result > 0)
                        return Json(new ResponseMessage() { IsSuccess = true });
                    return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = (int)result, Message = "添加失败" });
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 删除模块和子模块
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteBfModule(string id)
        {
            try
            {
                var result = (new BfModuleManager()).DeleteItemAndChildrenById(int.Parse(id));
                if (result)
                {
                    //更新moduleInfo的缓存信息
                    MemuTree.Instance.RefreshModuleInfo();

                    LogHelper.Info(
                         this.GetType(),
                         string.Format("用户[{0}]删除模块ID={1}", UserSession.Current.UserCode, id));
                    return Json(new ResponseMessage() { IsSuccess = true });
                }
                return Json(new ResponseMessage() { IsSuccess = false, ErrorCode = 0, Message = "删除失败" });

            }
            catch
            {
                return null;
            }
        }

        public bool DeleteOtherOperationById(string id)
        {
            var data = (new BfModuleManager()).DeleteOtherOperateByID(int.Parse(id));
            if (data)
                return true;
            return false;
        }

        public ActionResult SearchModuleOperate()
        {
            return View();
        }
    }
}
