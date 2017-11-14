using MB.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHPT.SystemInfo.Model
{
    public class QueryDto : PagedQueryParam
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public DateTime CreateTime { get; set; }
        public String UpdateUser { get; set; }
    }
}
