using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class ExceptionHandling2
    {
        public static void Run()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Cant do this") { Source = "t1"};                   //This exception will go unnoticed unless we're observing the task
            });
            Task t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this") { Source = "t2"};               //This exception will go unnoticed unless we're observing the task
            });

            Task.WaitAll(t1,t2);                                                            //Program will crash, and you'll get exceptions

            Console.WriteLine("Exception Handling Program done");
            Console.ReadKey();
        }
    }
}
