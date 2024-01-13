using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class MyTask2
    {
        public static void Write(Object o)
        {
            int i = 10000;
            while(i -- > 0)
            {
                Console.Write(o);
            }
        }

        public static void Run()
        {
            Task t2 = new Task(Write, "---");
            t2.Start();

            Task.Factory.StartNew(() => { Write("abc"); });

            var t = new Task(() => { Write("123"); });

            t.Start();

            Console.Write("Task 2 Program finished");
        }
    }
}
