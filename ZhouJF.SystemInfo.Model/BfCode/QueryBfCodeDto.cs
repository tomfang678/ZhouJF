using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfCode
{
    public class QueryBfCodeDto : PagedQueryParam
    {
        public string CODE { get; set; }

        public string DESCRIPTION { get; set; }
    }
}
