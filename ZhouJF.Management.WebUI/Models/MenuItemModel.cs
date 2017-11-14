using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.Models
{
    public class MenuItemModel
    {
        public int ID { get; set; }

        public string IconClass { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public List<MenuItemModel> Children { get; set; }
    }
}