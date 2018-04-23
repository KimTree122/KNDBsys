using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class PostData<T,S> 
    {
        public int MCount { get; set; }
        public List<T> DList { get; set; }
        public string Msg { get; set; }
        public S Obj { get; set; }
    }
}
