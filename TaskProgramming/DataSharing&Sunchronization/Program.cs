﻿using DataSharing_Sunchronization;

namespace DataSharing_Synchronization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CriticalSections.Run();
            Console.WriteLine("Main Program Finished");
            //Console.ReadKey();
        }
    }
}