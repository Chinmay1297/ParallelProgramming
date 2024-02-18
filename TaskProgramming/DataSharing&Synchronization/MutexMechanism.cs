using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    public class BankAccount4
    {
        private int balance;                    

        public int Balance { get => balance; set => balance = value; }

        public BankAccount4()
        {
            Balance = 0;
        }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }

        public void Transfer(BankAccount4 where, int amount)
        {
            Balance -= amount;
            where.Balance += amount;
        }
    }

    internal class MutexMechanism
    {
        public static void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount4();
            var ba2 = new BankAccount4();

            Mutex mutex = new Mutex();                            //It's a construct which controls access to a particular area of code

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();            //WaitOne() takes ms as parameter to define duration for which it should wait
                        try
                        {
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance: ${ba.Balance}");

            Console.ReadKey();
        }
    }
}
