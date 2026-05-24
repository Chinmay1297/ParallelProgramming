using ConcurrentCollections;
using System.Collections.Concurrent;

internal static class Program
{
    static void Main(string[] args)
    {
        //1. ConcurrentDictionary]
        ConcurrentDictionary.Run();

        Console.WriteLine("\nMain Program finished\n");
        //Console.ReadKey();
    }
}
