using System;
using System.Threading;

/*
Задача #1

Есть "сервер" в виде статического класса.  
У него есть переменная count (тип int) и два метода, которые позволяют эту переменную читать и писать: GetCount() и AddToCount(int value). 
К серверу стучатся множество параллельных клиентов, которые в основном читают, но некоторые добавляют значение к count. 

Нужно реализовать GetCount / AddToCount так, чтобы: 
читатели могли читать параллельно, без выстраивания в очередь по локу; 
писатели писали только последовательно и никогда одновременно; 
пока писатели добавляют и пишут, читатели должны ждать окончания записи. 

 */

namespace test.Server
{
    public static class Server
    {
        static int count;

        static ReaderWriterLockSlim @lock = new ReaderWriterLockSlim();

        static public int GetCount()
        {
            @lock.EnterReadLock();
            try
            {
                //выводы в консоль только для проверки
                Console.WriteLine("count = " + count);
                return count;
            }
            finally
            {
                @lock.ExitReadLock();
            }
        }

        static public void AddToCount(int value)
        {
            @lock.EnterWriteLock();
            try
            {
                Console.WriteLine("Пытаюсь добавить " + value);
                count += value;
                //Иметируем время на запись
                Thread.Sleep(1000);
                Console.WriteLine("Получилось " + count);
            }
            finally
            {
                @lock.ExitWriteLock();
            }
        }
    }
}
