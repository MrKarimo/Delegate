using System;
using System.Threading.Tasks;

/*
 
Задача #2

В .net есть возможность звать делегаты как синхронно: 
EventHandler h = new EventHandler(this.myEventHandler); 
h.Invoke(null, EventArgs.Empty); 
так и асинхронно:
var res = h.BeginInvoke(null, EventArgs.Empty, null, null);

Нужно реализовать возможность полусинхронного вызова делегата (написать реализацию класса AsyncCaller), который бы работал таким образом: 

EventHandler h = new EventHandler(this.myEventHandler); 
ac = new AsyncCaller(h); 
bool completedOK = ac.Invoke(5000, null, EventArgs.Empty);

"Полусинхронного" в данном случае означает, что делегат будет вызван, и вызывающий поток будет ждать, пока вызов не выполнится.
Но если выполнение делегата займет больше 5000 миллисекунд, то ac.Invoke выйдет и вернет в completedOK значение false.
 */

namespace test.Delegate
{
    class AsyncCaller
    {
        EventHandler _eventHandler;

        public AsyncCaller(EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }

        public bool Invoke(int milliseconds, object sender, EventArgs args)
        {
            var task = Task.Factory.StartNew(() => _eventHandler.Invoke(sender, args));
            return task.Wait(milliseconds);
        }

    }
}
