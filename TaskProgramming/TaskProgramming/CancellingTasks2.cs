using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class CancellingTasks2
    {
        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            //How to get notified when task is cancelled
            token.Register(() =>
            {  
                Console.WriteLine("Cancellation has been requested");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    token.ThrowIfCancellationRequested();             //short hand to cancel task
                    Console.Write($"{i++}\t");
                }
            },token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been released");
            });


            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("\nCancel Task Finished");
        }
    }
}
