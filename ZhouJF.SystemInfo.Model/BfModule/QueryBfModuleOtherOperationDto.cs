using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfModule
{
    public class QueryBfModuleOtherOperationDto : PagedQueryParam
    {
        public string CODE { get; set; }
        public int? BF_MODULE_ID { get; set; }
    }
}