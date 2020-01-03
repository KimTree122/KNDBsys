using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.ObserverPattern
{
    public class ConcreteSubject : Subject
    {
        private string _subjectState;

        /// <summary>
        /// Gets or sets subject state
        /// </summary>
        public string SubjectState
        {
            get { return _subjectState; }
            set { _subjectState = value; }
        }
    }
}
