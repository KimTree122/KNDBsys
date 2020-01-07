using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public class ConcreteSubject : Subject
    {
        public string SubjectState { get; set; }
        public Action SubjectAciont { get; set; }
    }
}
