using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfRegion
{
    public class QueryBfRegionDto : PagedQueryParam
    {
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string REGION_LEVEL { get; set; }
    }
}
