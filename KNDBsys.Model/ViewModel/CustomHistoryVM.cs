using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model.ViewModel
{
    public class CustomHistoryVM
    {
        public int id { get; set; }
        public string TypeName { get; set; }
        public string CheckDate { get; set; }
        public string Dicname { get; set; }
        public string FinishDate { get; set; }
        public int ServerStauts { get; set; }
    }
}
