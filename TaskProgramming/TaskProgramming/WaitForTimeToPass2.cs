using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class WaitForTimeToPass2
    {
        public static void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("To defuse Bomb press any key within 5 seconds. Time starts Now!!!");

                bool cancelled = cts.Token.WaitHandle.WaitOne(5000);            //you must cancel task within 5 seconds for this to return true

                Console.WriteLine(cancelled ? "Bomb was defused" : "BOOM!!!");
            },token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("WaitForTimeToPass2 Finished");
        }
    }
}
