using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfRole
{
    public class QueryBfRoleDto : PagedQueryParam
    {
        public string NAME { get; set; }
        public string CODE { get; set; }
    }
}