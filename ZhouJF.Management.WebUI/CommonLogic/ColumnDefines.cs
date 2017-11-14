using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHPT.Management.WebUI.CommonLogic
{
    public class ColumnDefines
    {
        public Type TypeInDT { get; set; }
        public string ColumnNameInDT {get;set;}

        public string ColumnNameInExcel { get; set; }
        public ColumnDefines(  string columnNameInExcel,string columnNameInDT,Type typeInDT) 
        {
            this.TypeInDT = typeInDT;
            this.ColumnNameInDT = columnNameInDT;
            this.ColumnNameInExcel = columnNameInExcel;
        }

    }
}