using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class ExceptionHandling4
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

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch(AggregateException ex)                                                //Aggregate exception stores all the exceptions from all the tasks
            {
                //Let say this function can only handle 1st exception
                ex.Handle(e =>                                                         //We're only handling 1 exception here, other one we need to handle separately
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine("Invalid operation!");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            

        }

        public static void Test()
        {
            try
            {
                Run();
            }
            catch(AggregateException ae)
            {
                foreach(var e in ae.InnerExceptions)
                {
                    Console.WriteLine($"Handled elsewhere: {e.GetType()} source {e.Source}");
                }
            }


            Console.WriteLine("Exception Handling Program done");
            Console.ReadKey();
        }
    }
}
