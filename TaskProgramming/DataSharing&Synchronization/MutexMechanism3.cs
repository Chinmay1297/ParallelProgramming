using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * NOTE: Mutexes are even more powerful than this because in addition to the kind of local mutexes that we've been using here,
 * a mutex can actually be a global construct that can be cross-process as an operating system construct.
 * Means we can have a mutex shared between several different programs.
 * So one of the reasons to have this functionality and to use it is to prevent several copies of the same program being executed.
**/
namespace DataSharing_Sunchronization
{
    internal class MutexMechanism3
    {
        public static void Run()
        {
            const string appName = "MyApp";
            Mutex mutex;

            try
            {
                mutex = Mutex.OpenExisting(appName);
                Console.WriteLine($"Sorry, {appName} is already running");
            }
            catch ( WaitHandleCannotBeOpenedException e)
            {
                Console.WriteLine("We can run the program just fine");
                mutex = new Mutex(false, appName);
            }

            Console.ReadKey();
            mutex.ReleaseMutex();

            //Try opening the .exe of this project multiple time(From bin/Debug): you'll get Sorry message from 2nd time.
        }
    }
}


