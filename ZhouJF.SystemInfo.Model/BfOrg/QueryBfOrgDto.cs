using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfOrg
{
    public class QueryBfOrgDto : PagedQueryParam
    {
        public string NAME { get; set; }
        public string CODE { get; set; }
        public int OWNER_ID { get; set; }
    }
}