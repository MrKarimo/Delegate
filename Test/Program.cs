using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using test.Server;
using test.Delegate;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tЗадача 1\n");
            //список "запросов"
            var tasks = new List<Task>();

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j< 10; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() => Server.GetCount()));
                }
                tasks.Add(Task.Factory.StartNew(() => Server.AddToCount(new Random().Next(10))));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("\nДля продолжения нажмите \"Enter\"");
            Console.ReadLine();

            Console.WriteLine("\n\tЗадача 2");

            var myEventHandler = new EventHandler((_sender, _args) => Thread.Sleep(4000));

            EventHandler h = new EventHandler(myEventHandler);
            var  ac = new AsyncCaller(h);

            Console.WriteLine("\nEvent: 4000 ms\n Timer: 5000 ms");
            var completedOK = ac.Invoke(5000, null, EventArgs.Empty);
            Console.WriteLine("Результат: " + completedOK.ToString());

            Console.WriteLine("\nEvent: 4000 ms\n Timer: 3000 ms");
            completedOK = ac.Invoke(3000, null, EventArgs.Empty);
            Console.WriteLine("Результат: " + completedOK.ToString());

            Console.ReadLine();

        }
    }
}

