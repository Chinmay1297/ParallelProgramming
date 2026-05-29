using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCoordination
{
    internal class ChildTask
    {
        public static void Run()
        {
            var parent = new Task(() =>
            {
                Console.WriteLine("Parent task starting");

                //detached if we dont attach to parent
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                });

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
