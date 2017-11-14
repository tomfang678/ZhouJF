namespace YHPT.Management.WebUI.Library
{
    /// <summary>
    ///     角色模块权限控制
    /// </summary>
    public class ModuleOperationInfo
    {
        /// <summary>
        ///     模块编码
        /// </summary>
        public string MODULE_CODE { get; set; }

        /// <summary>
        ///     模块名称
        /// </summary>
        public string MODULE_NAME { get; set; }

        /// <summary>
        ///     模块地址
        /// </summary>
        public string MODULE_URL { get; set; }

        /// <summary>
        ///     权限操作编码
        /// </summary>
        public string OPERATION_CODE { get; set; }
    }
}