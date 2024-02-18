using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * ReaderWriter locks allows you to read concurrently, but locks when writing
*/
namespace DataSharing_Sunchronization
{
    internal class ReaderWriterLocks
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();
        static Random random = new Random();

        public static void Run()
        {
            int x = 0;
            var tasks = new List<Task>();
            
            for(int i=0; i<10; i++)
            {
                tasks.Add(Task.Run(() =>                                                 //This other thread is just reading
                {
                    padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {0}");

                    Thread.Sleep(5000);
                    padlock.ExitReadLock();

                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }
            
            while(true)                                                                 //Main thread is modifying
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write lock acquired");
                int newValue = random.Next(10);
                x = newValue;

                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("write lock released");
            }

        }
    }
}
