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
            InterlockedOperations.Run();


            Console.WriteLine("Main Program Finished");
            //Console.ReadKey();
        }
    }
}