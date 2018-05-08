using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model.ViewModel
{
    public class CheckInViewModel
    {
        public int id { get; set; }
        public string CustomName { get; set; }
        public string ServerTypeName { get; set; }
        public int Stauts { get; set; }
        public string CheckInDate { get; set; }
    }
}
