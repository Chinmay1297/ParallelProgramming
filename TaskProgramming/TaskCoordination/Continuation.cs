using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class Continuation
    {
        public static void Run()
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling Water");
            });

            //ContinueWith method is used to create a continuation task that will be executed when the antecedent task completes.
            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour water into cup");
            });

            task2.Wait();
        }
    }
}
