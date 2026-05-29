using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class Continuation2
    {
        public static void Run()
        {
            var task1 = Task.Factory.StartNew(()=> "Task 1 completed");
            var task2 = Task.Factory.StartNew(()=> "Task 2 completed");

            //ContinueWhenAll method is used to create a continuation task that will be executed when all of the antecedent tasks have completed.
            var task3 = Task.Factory.ContinueWhenAll(new[] { task1, task2 }, tasks =>
            {
                foreach(var t in tasks)
                {
                    Console.WriteLine(t.Result);
                }
                Console.WriteLine("All tasks completed, now executing task 3");
            });

            task3.Wait();
        }
    }
}
