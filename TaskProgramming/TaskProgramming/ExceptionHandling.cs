using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class ExceptionHandling
    {
        public static void Run()
        {
            Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException();                          //This exception will go unnoticed unless we're observing the task
            });

            Console.WriteLine("Exception Handling Program done");
            Console.ReadKey();
        }
    }
}
