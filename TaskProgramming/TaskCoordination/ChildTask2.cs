using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class ChildTask2
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
                }, TaskCreationOptions.AttachedToParent);

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
