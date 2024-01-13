using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class MyTask1
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Run()
        {
            Task.Factory.StartNew(() => Write('.'));  //creating a task and starting it simultaneously

            var t = new Task(() => Write('?'));        //creating a task variable which is yet to start

            t.Start();

            Write('-');

            //output wont be sequencial, as all 3 tasks run in different threads.

            Console.Write("Task Program done");
        }
    }
}
