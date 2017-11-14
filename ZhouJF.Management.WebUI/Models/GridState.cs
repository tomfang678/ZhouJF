using YHPT.Management.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.Models
{
    public class GridStage
    {
        public string tableId{get;set;}
        public string filterForm{get;set;}
        public List<NameValueObject> paramArray { get; set; }
        public int startIndex { get; set; }
        public int pageSize { get; set; }
        public int sortFieldIndex { get; set; }
        public string sortDiection { get; set; }
    }
}