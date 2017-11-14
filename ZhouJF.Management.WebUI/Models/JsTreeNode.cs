using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.Models
{
    public class JsTreeNode
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public int? sort_no { get; set; }
        public State state { get; set; }
    }

    public class State
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
    }
}