using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfModule
{
    public class QueryBfModuleDto : PagedQueryParam
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public int? PARENT_ID { get; set; }
    }
}