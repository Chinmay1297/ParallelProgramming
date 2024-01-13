using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class CompositeCancellationToken
    {
        public static void Run()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.Write($"{i++}\t");
                    Thread.Sleep(500);
                }
            }, paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();         //planned, preventative, emergency any of those will cause paranoid to provoke cancellation as all are linked to paranoid

            Console.WriteLine("CompositeCancellationToken Program done");
        }
    }
}
