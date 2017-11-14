using System;
using System.Collections.Generic;

namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     模块管理
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        ///     ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///     模块编码
        /// </summary>
        public string MODULE_CODE { get; set; }

        /// <summary>
        ///     模块名称
        /// </summary>
        public string MODULE_NAME { get; set; }

        /// <summary>
        ///     父级ID
        /// </summary>
        public int? PARENT_ID { get; set; }

        /// <summary>
        ///     创建人
        /// </summary>
        public string REMARK { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public string CREATE_USER { get; set; }

        /// <summary>
        ///     页面Url
        /// </summary>
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        ///     最后修改人
        /// </summary>
        public string LAST_MODIFIED_USER { get; set; }

        /// <summary>
        ///     最后修改时间
        /// </summary>
        public DateTime? LAST_MODIFIED_TIME { get; set; }

        /// <summary>
        ///     模块地址
        /// </summary>
        public string MODULE_URL { get; set; }

        /// <summary>
        ///     模块序号
        /// </summary>
        public int? MODULE_ORDER { get; set; }

        /// <summary>
        ///     模块图标
        /// </summary>
        public string MODULE_ICON { get; set; }

        /// <summary>
        ///     是否需要权限控制
        /// </summary>
        public bool IsNeedControl { get; set; }

        /// <summary>
        ///     子列表
        /// </summary>
        public List<ModuleInfo> Items { get; set; }

        /// <summary>
        ///     程序里模块ID
        /// </summary>
        public int? SystemID { get; set; }
    }
}