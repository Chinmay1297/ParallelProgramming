using DataSharing_Sunchronization;

namespace DataSharing_Synchronization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Scenario to demonstrate the need for critical section
            //CriticalSections.Run();

            //2. How to implement critical sections using lock(obj)
            //CriticalSections.Run();

            //3. Different way to achieve synchronization using Interlocked class's methods
            //InterlockedOperations.Run();

            //4. Spin Locking: Works well in the scenarios where acquiring a lock fails/or timeout.
            //SpinLockMechanism.Run();

            //5. Lock Recursion: new SpinLock(int x) accepts an argument which decides whether to enavle task owner tracking
            //LockRecursion.Run();

            //6. Mutex - Similar to lock: controls access to a particular region
            //MutexMechanism.Run();

            //7. Locking multiple mutexes together
            //MutexMechanism2.Run();

            //8. Global Mutex shared across multiple programs
            MutexMechanism3.Run();

            Console.WriteLine("Main Program Finished");
            //Console.ReadKey();
        }
    }
}