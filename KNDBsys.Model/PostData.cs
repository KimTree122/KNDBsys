using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Model
{
    public class PostData<L,E> 
    {
        public int total { get; set; }
        public List<L> rows { get; set; }
        public string Msg { get; set; }
        public E Entity { get; set; }
    }
}
