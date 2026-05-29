using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class Continuation3
    {
        static Random random = new Random();
        public static void Run()
        {
            var task1 = Task.Factory.StartNew(()=>
            {
                Thread.Sleep(random.Next(2000)); // Simulate some work
                return "Task 1 completed";
            });
            var task2 = Task.Factory.StartNew(()=>
            {
                Thread.Sleep(random.Next(2000));
                return "Task 2 completed";
            });

            //ContinueWhenAny method is used to create a continuation task that will be executed when any of the antecedent tasks have completed.
            var task3 = Task.Factory.ContinueWhenAny(new[] { task1, task2 }, task =>
            {
                Console.WriteLine(task.Result);
                Console.WriteLine("All tasks completed, now executing task 3");
            });

            task3.Wait();
        }
    }
}
