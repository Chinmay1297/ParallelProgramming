using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class CancellingTasks
    {
        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            //How to get notified when task is cancelled
            token.Register(() =>
            {   
                //Do stuff to get notified
                Console.WriteLine("Cancellation has been requested");
            });

            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if cancellation of task is requested, 2 ways of cancellling it:
                    if(token.IsCancellationRequested)
                    {
                        //break;                                        //1st way                  --> Task wont know its cancelled, it will be treated as successful
                        throw new OperationCanceledException();         //2nd way - Recommended    --> Task will have a cancelled status
                    }
                    //token.ThrowIfCancellationRequested();             //Instead of whole if block u can use this short hand
                    else
                    {
                        Console.Write($"{i++}\t");
                    }
                }
            },token);

            t.Start();

            //cancel the task on press of input key from user
            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("\nCancel Task Finished");
        }
    }
}
