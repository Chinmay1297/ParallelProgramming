using ConcurrentCollections;
using System.Collections.Concurrent;

internal static class Program
{
    static void Main(string[] args)
    {
        //1. ConcurrentDictionary]
        //ConcurrentDictionary.Run();

        //2. ConcurrentQueue
        //ConcurrentQueue.Run();

        //3. ConcurrentStack
        //ConcurrentStack.Run();

        //4. ConcurrentBag
        //ConcurrentBag.Run();

        //5. Producer-Consumer
        Producer_Consumer.Run();

        Console.WriteLine("\nMain Program finished\n");
        //Console.ReadKey();
    }
}
