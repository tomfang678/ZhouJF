using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Framework.Common;

namespace YHPT.SystemInfo.Model.BfCode
{
    public class QueryBfCodeDetailDto : PagedQueryParam
    {
        public int MainId { get; set; }

        public string Code { get; set; }
    }
}
