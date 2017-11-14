using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using MB.Framework.Common;
using MB.Framework.Common.Logging;
using YHPT.SystemInfo.Business;
using YHPT.SystemInfo.Model.BfModule;

namespace YHPT.Management.WebUI.Library
{
    public class MemuTree
    {
        private const string AllMenuItem = "allMenuItem";
        private static MemuTree _Instance;
        //private string MenuTreePath = CommonHelper.GetAppValue("MenuTree"); //@"Content\MenuTree.xml";

        private MemuTree()
        {
        }

        /// <summary>
        ///     缓存容易的实例
        /// </summary>
        public static MemuTree Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (typeof (MemuTree))
                    {
                        if (_Instance == null)
                            _Instance = new MemuTree();
                    }
                }
                return _Instance;
            }
        }

        /// <summary>
        ///     根据RoleCode获取授权访问的Module
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        public List<BfModuleInfo> GetModuleInfo(bool refresh = false)
        {
            if (refresh)
            {
                //去掉所有树节点缓存
                RefreshModuleInfo();
                //去掉Session里用户展示菜单
                RefreshUserModuleMapping();
            }

            if (UserSession.Current.DisplayModuleInfo != null)
                return UserSession.Current.DisplayModuleInfo;

            var allMenuTree = GetMenuItem();

            SetRoleModule(allMenuTree);
            //管理员用户返回所有节点
            if (UserSession.Current.IsAdmin)
            {
                UserSession.Current.DisplayModuleInfo = allMenuTree;
                return allMenuTree;
            }

            return GetDisplayMenuItem(allMenuTree);
        }

        private void SetRoleModule(List<BfModuleInfo> allMenuTree)
        {
            if (allMenuTree != null)
            {
                foreach (var menu in allMenuTree)
                {
                    if (menu.Items == null) continue;
                    foreach (var role in UserSession.Current.RoleOperation)
                    {
                        var node = menu.Items.Find(a => a.CODE == role.MODULE_CODE);
                        if (node != null)
                        {
                            role.MODULE_NAME = node.NAME;
                            role.MODULE_URL = node.URL;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<BfModuleInfo> GetMenuItem()
        {
            //优先读缓存，缓存没有读取配置文件
            var obj = CacheHelper.Get(AllMenuItem);
            if (obj != null)
                return obj as List<BfModuleInfo>;

            #region 读取配置文件

            var moduleItem = new List<BfModuleInfo>();
            try
            {
                #region 读取数据库菜单

                var bfModuleList = (new BfModuleManager()).GetItems(new QueryBfModuleDto());
                moduleItem = GetTreeModuleByParentId(bfModuleList, 0);
                if (moduleItem == null)
                    moduleItem = new List<BfModuleInfo>();

                #endregion

                
                #region 读取XML
                /*
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MenuTreePath);

                if (File.Exists(path) == false)
                {
                    LogHelper.Error(this.GetType(), string.Format("未找到指定的配置文件:{0}", path),new IOException());
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                var Module = doc.SelectNodes("/Modules/Module");
                if (Module != null)
                {
                    foreach (XmlNode moduleNode in Module)
                    {
                        var model = new BfModuleInfo
                        {
                            ID = Convert.ToInt32(moduleNode.Attributes["Id"].Value),
                            CODE = moduleNode.Attributes["MODULE_CODE"].Value,
                            NAME = moduleNode.Attributes["MODULE_NAME"].Value,
                            URL = moduleNode.Attributes["MODULE_URL"].Value,
                            ORDER = Convert.ToInt32(moduleNode.Attributes["MODULE_ORDER"].Value),
                            ICON = moduleNode.Attributes["MODULE_ICON"].Value,
                            PARENT_ID = Convert.ToInt32(moduleNode.Attributes["PARENT_ID"].Value),
                            IsNeedControl = moduleNode.Attributes["IsNeedControl"] == null || Convert.ToBoolean(moduleNode.Attributes["IsNeedControl"].Value)
                        };

                        var menuList = moduleNode.ChildNodes;
                        if (menuList != null)
                        {
                            var menuItem = new List<BfModuleInfo>();
                            foreach (XmlNode menuNode in menuList)
                            {
                                var menu = new BfModuleInfo
                                {
                                    ID = Convert.ToInt32(menuNode.Attributes["Id"].Value),
                                    CODE = menuNode.Attributes["MODULE_CODE"].Value,
                                    NAME = menuNode.Attributes["MODULE_NAME"].Value,
                                    URL = menuNode.Attributes["MODULE_URL"].Value,
                                    ORDER = Convert.ToInt32(menuNode.Attributes["MODULE_ORDER"].Value),
                                    ICON = menuNode.Attributes["MODULE_ICON"].Value,
                                    PARENT_ID = Convert.ToInt32(menuNode.Attributes["PARENT_ID"].Value),
                                    IsNeedControl = menuNode.Attributes["IsNeedControl"] == null || Convert.ToBoolean(menuNode.Attributes["IsNeedControl"].Value)
                                };
                                menuItem.Add(menu);
                            }
                            model.Items = menuItem;
                        }
                        else
                        {
                            model.Items = new List<BfModuleInfo>();
                        }
                        moduleItem.Add(model);
                    }
                }
                 */
                #endregion
                
            }
            catch (Exception ex)
            {
                LogHelper.Error(GetType(), "", ex);
            }

            //放到缓存内
            CacheHelper.Insert(AllMenuItem, moduleItem);

            return moduleItem;

            #endregion
        }

        /// <summary>
        ///     递归遍历集合
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<BfModuleInfo> GetTreeModuleByParentId(IEnumerable<BfModuleInfo> lst, int parentId)
        {
            var currentLst = lst.Where(p => p.PARENT_ID == parentId);
            if (currentLst != null)
            {
                foreach (var module in currentLst)
                {
                    var childModules = GetTreeModuleByParentId(lst, module.ID);
                    if (childModules != null && childModules.Count > 0)
                    {
                        module.Items = childModules;
                    }
                }
                return currentLst.ToList();
            }
            return null;
        }

        /// <summary>
        ///     获取需要展示的菜单
        /// </summary>
        /// <param name="allMenuTree">全部菜单</param>
        /// <returns></returns>
        private List<BfModuleInfo> GetDisplayMenuItem(List<BfModuleInfo> allMenuTree)
        {
            if (allMenuTree == null || allMenuTree.Count == 0) return new List<BfModuleInfo>();

            var displayMenu = new List<BfModuleInfo>();
            foreach (var menuTree in allMenuTree)
            {
                //判断父节点是否有不需要权限控制的,如果有，则展示该父节点所有节点
                if (!menuTree.IsNeedControl)
                {
                    displayMenu.Add(menuTree);
                    continue;
                }

                //如果子节点有不需要权限控制的，将该子节点的父节点和该子节点一起展示
                //如果有该子节点访问权限，将该子节点的父节点和该子节点一起展示
                if (menuTree.Items != null)
                {
                    foreach (var nodeTree in menuTree.Items)
                    {
                        if (!nodeTree.IsNeedControl ||
                            UserSession.Current.ModuleInfos.Exists(p => p.MODULE_CODE == nodeTree.CODE))
                        {
                            //添加父节点
                            if (!displayMenu.Exists(p => p.ID == menuTree.ID))
                            {
                                var menu = new BfModuleInfo();
                                menu.ID = menuTree.ID;
                                menu.IsNeedControl = menuTree.IsNeedControl;
                                menu.NAME = menuTree.NAME;
                                menu.ICON = menuTree.ICON;
                                menu.CODE = menuTree.CODE;
                                menu.ORDER = menuTree.ORDER;
                                menu.URL = menuTree.URL;
                                menu.PARENT_ID = menuTree.PARENT_ID;
                                displayMenu.Add(menu);
                            }
                            //添加子节点
                            var parentMenu = displayMenu.FirstOrDefault(p => p.ID == menuTree.ID);
                            if (parentMenu.Items == null || !parentMenu.Items.Exists(p => p.ID == nodeTree.ID))
                            {
                                if (parentMenu.Items == null) parentMenu.Items = new List<BfModuleInfo>();
                                parentMenu.Items.Add(nodeTree);
                            }
                        }
                    }
                }
                
            }

            UserSession.Current.DisplayModuleInfo = displayMenu;

            return displayMenu;
        }

        /// <summary>
        ///     更新缓存的Module信息
        /// </summary>
        public void RefreshModuleInfo()
        {
            CacheHelper.Remove(AllMenuItem);
        }

        /// <summary>
        ///     更新用户模块信息
        /// </summary>
        public void RefreshUserModuleMapping()
        {
            UserSession.Current.DisplayModuleInfo = null;
        }
    }
}