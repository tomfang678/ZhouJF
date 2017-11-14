using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfUser
{
    public class QueryBfUserDto : PagedQueryParam
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public int OWNER_ID { get; set; }
    }
}