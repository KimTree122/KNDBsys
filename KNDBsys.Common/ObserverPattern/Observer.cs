using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public abstract class Observer
    {
        public Action<string> obaction;
        public abstract void ToDo();
    }
}
