using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model.ViewModel.WebVM
{
    public class WebTreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public bool Checked { get; set; }
        public string state { get; set; }
        public Dictionary<string, string> attributes { get; set; }
        public object children { get; set; }
    }

    public class WebTexValue
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

}
