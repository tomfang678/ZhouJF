using System.Collections.ObjectModel;
using MB.Framework.Common.Enums;

namespace YHPT.Management.WebUI.Library.Controls
{
    /// <summary>
    ///     jauery Datatables Ajax异步数据请求参数
    /// </summary>
    public class JQueryDataTablesModel
    {
        /// <summary>
        ///     DataTables 用来生成的信息
        /// </summary>
        public int GridKey { get; set; }

        /// <summary>
        ///     显示的起始索引(从0开始)
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        ///     当前页码(从0开始)
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        ///     显示的行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     排序的列名称
        /// </summary>
        public string SortField { get; set; }

        /// <summary>
        ///     排序方向.
        /// </summary>
        public SortDirection Direction { get; set; }

        /// <summary>
        ///     Datatables 显示的列名
        /// </summary>
        public ReadOnlyCollection<string> FieldNames { get; set; }
    }
}