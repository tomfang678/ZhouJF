using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfRole
{
    public class QueryBfRolePermissionDto : PagedQueryParam
    {
        public int BF_ROLE_ID { get; set; }
        public int BF_MODULE_OPERATION_ID { get; set; }
    }
}