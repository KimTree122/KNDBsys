using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public class ObserverTest
    {
        public string ObserverToDo()
        {
            ConcreteSubject s = new ConcreteSubject();
            //s.Attach(new ConcreteObserver(s, "X"));
            //s.Attach(new ConcreteObserver(s, "Y"));
            //s.Attach(new ConcreteObserver(s, "Z"));
            //s.SubjectState = "abc";
            //s.Notify();

            //s.Attach(new ConcreteObserver("A", ObserverAction));
            //s.Attach(new ConcreteObserver("B", ObserverAction));
            //s.SubjectState = "abc";
            //s.Notify();

            s.Attach(new ConcreteObserver("A", ObserverParams));
            s.Attach(new ConcreteObserver("B", ObserverParams));
            s.Detach(new ConcreteObserver("A", ObserverParams));
            s.Notify();
            return "success";
        }

        private void ObserverParams(string observerName)
        {
            string msg = string.Format(" Observer '{0}' reciver new state ", observerName);
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void ObserverAction()
        {
            System.Diagnostics.Debug.WriteLine(" Observer  reciver new state ");
        }
    }
}
