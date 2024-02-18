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

            //5.
            LockRecursion.Run();

            Console.WriteLine("Main Program Finished");
            //Console.ReadKey();
        }
    }
}