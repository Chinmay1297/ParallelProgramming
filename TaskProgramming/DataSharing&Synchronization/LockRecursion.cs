using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    internal class LockRecursion
    {
        static SpinLock sl = new SpinLock(true);        //This argument decides whether spinlock should enable thread owner tracking

        public static void lockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch(LockRecursionException e)
            {
                Console.WriteLine("Exception: "+e.ToString());          //Since spinlock doesnt allow lock recursion, you'll get exception here
            }
            finally
            {
                if(lockTaken)
                {
                    Console.WriteLine($"Took a lock, x = {x}");
                    lockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }

        public static void Run()
        {
            lockRecursion(5);
        }
    }
}
