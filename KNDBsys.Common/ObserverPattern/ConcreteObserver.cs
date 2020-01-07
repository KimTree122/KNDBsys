using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public class ConcreteObserver : Observer
    {
        private string _subName;

        //private string _name;
        //private string _observerState;
        //private ConcreteSubject _subject;

        //public ConcreteObserver(ConcreteSubject subject, string name)
        //{
        //    this._subject = subject;
        //    this._name = name;
        //}

        public ConcreteObserver(string subName,Action<string> action)
        {
            _subName = subName;
            obaction = action;
        }

        //public override void Update()
        //{
        //    _observerState = _subject.SubjectState;
        //    Console.WriteLine("Observer {0}'s new state is {1}", _name, _observerState);
        //}


        public override void ToDo()
        {
            obaction.Invoke(_subName);
        }
    }
}
