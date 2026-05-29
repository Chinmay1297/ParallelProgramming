using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class ChildTask3
    {
        public static void Run()
        {
            var parent = new Task(() =>
            {
                Console.WriteLine("Parent task starting");

                //TaskCreationOptions.AttachedToParent attaches the child task to the parent task
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                    throw new Exception();      //To make it run on the fault handler
                }, TaskCreationOptions.AttachedToParent);

                //TaskContinuationOptions.AttachedToParent attaches the continuation task to the parent task,
                //and TaskContinuationOptions.OnlyOnRanToCompletion specifies that the continuation task will only be executed if the antecedent task ran to completion without throwing an exception.
                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                //TaskContinuationOptions.AttachedToParent attaches the continuation task to the parent task,
                //and TaskContinuationOptions.OnlyOnFaulted specifies that the continuation task will only be executed if the antecedent task threw an exception.
                var failureHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Oh no, task {t.Id}'s state is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
    }
}
