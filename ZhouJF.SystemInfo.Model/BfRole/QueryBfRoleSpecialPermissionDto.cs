using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfRole
{
    public class QueryBfRoleSpecialPermissionDto : PagedQueryParam
    {
        public int BF_ROLE_ID { get; set; }
        public int BF_MODULE_SPECIAL_ID { get; set; }
    }
}