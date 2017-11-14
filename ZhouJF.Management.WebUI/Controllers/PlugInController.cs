using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Library.Controls;
using YHPT.Management.WebUI.Models;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfOrg;

namespace YHPT.Management.WebUI.Controllers
{
    public class PlugInController : BaseController
    {
        #region 部门

        public PartialViewResult BfOrgTree()
        {
            return PartialView();
        }

        public PartialViewResult BfOrg()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult SearchBfOrg(JQueryDataTablesModel jQueryDataTablesModel, BfOrgInfo bfOrgInfo)
        {
            var queryParam = new QueryBfOrgDto
            {
                PageIndex = jQueryDataTablesModel.PageIndex,
                PageSize = jQueryDataTablesModel.PageSize,
                CODE = bfOrgInfo.CODE,
                NAME = bfOrgInfo.NAME,
                OWNER_ID = bfOrgInfo.OWNER_ID,
                SortField = jQueryDataTablesModel.SortField,
                SortDirection = jQueryDataTablesModel.Direction
            };
            var pageList = (new BfOrgManager()).GetPagedList(queryParam);
            return this.DataTablesJson(pageList, jQueryDataTablesModel.GridKey);
        }

        public ActionResult _BfOrgSelector(string key, int limit = 10)
        {
            var bfOrgList = (new BfOrgManager()).GetItems(new QueryBfOrgDto());
            var boxSpec = bfOrgList.FindAll(o => (
                o.CODE.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0 ||
                o.NAME.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0
                ));

            var data = boxSpec.Take(limit).Select(o => new
            {
                id = o.ID.ToString(),
                name = string.Format("{0}[{1}]", o.NAME, o.CODE)
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllOrgByTree()
        {
            
            var bfOrgs = (new BfOrgManager()).GetAllOrgByCombin();
            var nodes = ConvertOrgToNode(bfOrgs);
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        private List<Node> ConvertOrgToNode(List<BfOrgInfo> orgList)
        {
            if (orgList == null || orgList.Count < 1)
                return null;
            var nodes = new List<Node>();
            foreach (var org in orgList)
            {
                nodes.Add(new Node()
                {
                    text = org.NAME,
                    nodeId = org.ID,
                    id = org.ID,
                    code = org.CODE,
                    isSelected = false,
                    nodes = ConvertOrgToNode(org.CHILDS)
                    });
            }
            return nodes;
        }
        #endregion

        #region Import

        public PartialViewResult ImportData()
        {
            return PartialView();
        }
        #endregion
    }
}
