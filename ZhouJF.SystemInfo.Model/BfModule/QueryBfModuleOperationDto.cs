using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfModule
{
    public class QueryBfModuleOperationDto : PagedQueryParam
    {
        public string CODE { get; set; }
        public int? BF_MODULE_ID { get; set; }
    }
}