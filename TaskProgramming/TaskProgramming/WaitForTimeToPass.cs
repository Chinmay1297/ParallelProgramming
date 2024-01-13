using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProgramming
{
    internal class WaitForTimeToPass
    {
        public static void Run()
        {
            var t = new Task(() =>
            {
                Thread.Sleep(1000);             //Pause current task --> Scheduler can work on other task meanwhile
                //Thread.SpinWait(500);           //Also pause the thread --> Scheduler isn't going to get other task executing (Wasting CPU cycles)
                //SpinWait.SpinUntil();         //For context switching Spinwait is more beneficial than Thread.sleep

            });
        }
    }
}
