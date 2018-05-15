using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class PostData<T,E> 
    {
        public int MCount { get; set; }
        public List<T> DList { get; set; }
        public string Msg { get; set; }
        public E Obj { get; set; }
    }
}
