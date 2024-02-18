using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Let say you have declared padlock.EnterReadLock() but then inside it, you're trying to upgrade to write lock. Then you'll receive error.
 * To get this working we have something called upgradablereaderwriterlock
*/
namespace DataSharing_Sunchronization
{
    internal class UpgradableReaderWriterLocks
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim();        //you can pass LockRecursionPolicy.SupportsRecursion to allow nested read locks, without passing this you'll get error
        static Random random = new Random();

        public static void Run()
        {
            int x = 0;
            var tasks = new List<Task>();
            
            for(int i=0; i<10; i++)
            {
                tasks.Add(Task.Run(() =>                                                 //This other thread is just reading
                {
                    padlock.EnterUpgradeableReadLock();
                    Console.WriteLine($"Entered read lock, x = {0}");

                    padlock.EnterWriteLock();
                    if(i%2 == 0)
                    {
                        x += 2;
                    }
                    padlock.ExitWriteLock();

                    Thread.Sleep(5000);
                    padlock.ExitUpgradeableReadLock();

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
