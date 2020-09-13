using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.RxNet
{
    public class Observable
    {
        public interface IObservable<out T>
        {
            IDisposable Subscribe(IObserver<T> observer);
            
        }

        public interface IObserver<in T>
        {
            void OnCompleted();
            void OnError(Exception error);
            void OnNext(T value);
        }
    }
}
