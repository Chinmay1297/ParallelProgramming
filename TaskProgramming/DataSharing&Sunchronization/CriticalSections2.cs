using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    public class BankAccount1
    {
        //Critical section is a piece of code that marks the area, such that only one thread can enter it at a time

        public object padlock = new object();
        public int Balance { get; set; }

        public BankAccount1()
        {
            Balance = 0;
        }

        public void Deposit(int amount)
        {
            lock (padlock)                          //if other thread t2 comes here while t1 has locked it, t2 will wait for t1 to finish
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
        }
    }

    internal class CriticalSections2
    {
        public static void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount1();

            for(int i=0; i<10; i++)
            {
                //Depositing 100 amount to bank 1000 times
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                //Withdrawing 100 amount from bank 1000 times
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance: ${ba.Balance}");             

            Console.ReadKey();
        }
    }
}
