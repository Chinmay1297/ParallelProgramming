using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    internal class MutexMechanism2
    {
        public static void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount4();
            var ba2 = new BankAccount4();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();                            


            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(1);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>                                       //Transfering 1000$ from acct ba to acct ba2
                {
                    for(int j=0; j<1000; j++)
                    {
                        bool haveLock = Mutex.WaitAll(new[] { mutex, mutex2 });             //if both mutex's are available then only u can acquire this lock
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();
                            }
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance: ${ba.Balance}");
            Console.WriteLine($"Final balance: ${ba2.Balance}");

            Console.ReadKey();
        }
    }
}

/**
 * NOTE: Mutexes are even more powerful than this because in addition to the kind of local mutexes that we've been using here,
 * a mutex can actually be a global construct that can be cross-process as an operating system construct.
 * Means we can have a mutex shared between several different programs.
 * So one of the reasons to have this functionality and to use it is to prevent several copies of the same program being executed.
**/
