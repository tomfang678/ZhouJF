using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YHPT.Management.WebUI.Library;
using YHPT.Management.WebUI.Models;

namespace YHPT.Management.WebUI.Controllers
{
    public class SharedController : BaseController
    {

        [ChildActionOnly]
        public ActionResult _TopNavMenu()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _LeftNavMenu()
        {
            var pagePath = HttpContext.Items[Request_Path_Item_Key] as string;
            var menuItemPath = HttpContext.Items[Menu_Path_Item_Key] as string;

            var modules = MemuTree.Instance.GetModuleInfo();

            var model = new List<MenuItemModel>();

            foreach (var item in modules.Where(o => o.PARENT_ID == null || o.PARENT_ID == 0).OrderBy(p => p.ORDER))
            {
                var node = new MenuItemModel()
                {
                    IconClass = item.ICON,
                    ID = item.ID,
                    IsActive = item.URL.ToLower().IndexOf(pagePath) == 0,
                    Text = item.NAME,
                    Url = item.URL,
                    Children = new List<MenuItemModel>()
                };
                if (item.Items != null)
                {
                    node.Children = item.Items.OrderBy(o => o.ORDER).Select(o =>
                        new MenuItemModel
                        {
                            Children = null,
                            IconClass = null,
                            ID = o.ID,
                            IsActive = String.IsNullOrEmpty(o.URL) ? false : o.URL.ToLower().IndexOf(pagePath) == 0,
                            Text = o.NAME,
                            Url = o.URL
                        }).ToList();
                    if (node.Children.Exists(o => o.IsActive))
                    {
                        node.IsActive = true;
                    }
                    else if (string.IsNullOrEmpty(menuItemPath) == false)
                    {
                        var activeItem = node.Children.FirstOrDefault(o => String.IsNullOrEmpty(o.Url) ? false : o.Url.ToLower().IndexOf(menuItemPath) == 0);
                        if (activeItem != null)
                        {
                            activeItem.IsActive = true;
                            node.IsActive = true;
                        }
                    }
                }
                model.Add(node);
            }

            return View(model);
        }
    }
}
