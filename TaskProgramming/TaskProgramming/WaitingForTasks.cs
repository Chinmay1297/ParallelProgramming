using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class WaitingForTasks
    {
        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                int i = 0;
                while (i <= 5)
                {
                    Console.WriteLine(i);
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                    i++;
                }

                Console.WriteLine("I'm done");
            }, token);

            t.Start();

            //t.Wait();               //wait for the task to finish
            //t.Wait(token);          //if we provide token, that way it wait will know its finished earlier

            Task t2 = Task.Factory.StartNew(() => { Thread.Sleep(3000); },token);

            //Task.WaitAll(t,t2);      //When you want for all the tasks to finish ---> total time taken 7000 ms because its the longer one

            Task.WaitAny(t,t2);      //Wait till any of the tasks finishes

            //var timeOut = 2000;
            //Task.WaitAny(new[] {t, t2}, timeOut, token);    //It will wait till 2 seconds for either of those tasks to finish

            //Dont Cancel when using WaitAny/WaitAll -> it will throw unhandled exception and will crash program -> u need to handle it separately

            Console.WriteLine($"Task t1 status: {t.Status}");
            Console.WriteLine($"Task t2 status: {t2.Status}");

            Console.WriteLine("WaitingForTasks Program Finished");
        }
    }
}
