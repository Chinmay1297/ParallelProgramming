using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    public class BankAccount3
    {
        private int balance;                    //(Backing Field)This is required to get a ref to it (Because Interlocked.Add() expects a reference)

        public int Balance { get => balance; set => balance = value; }

        public BankAccount3()
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
    }

    internal class SpinLockMechanism
    {
        public static void Run()
        {
            var ba = new BankAccount3();

            var tasks = new List<Task>();

            System.Threading.SpinLock sl = new System.Threading.SpinLock();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        bool lockTaken = false;
                        try
                        {
                            sl.Enter(ref lockTaken);
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if(lockTaken) sl.Exit();
                        }
                    }
                }));

                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        bool lockTaken = false;                     //Will be used to monitor whether lock was acquired or not.
                        try
                        {
                            sl.Enter(ref lockTaken);                //This might timeout - Ex: When two threads trying to acquire eachother's lock, it will result in deadlock. If lock is acquired: lockTaken will be true
                            ba.Withdraw(100);                       //Withdraw only when lock was acquired.
                        }
                        finally
                        {
                            if (lockTaken) sl.Exit();               //If it was successful to acquire a lock, then successfully exit lock
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
