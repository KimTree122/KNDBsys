using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public class ConcreteObserver : Observer
    {
        private string _subName;
        
        public ConcreteObserver(string subName,Action<string> action)
        {
            _subName = subName;
            obaction = action;
        }

        public override void ToDo()
        {
            obaction.Invoke(_subName);
        }
    }
}
