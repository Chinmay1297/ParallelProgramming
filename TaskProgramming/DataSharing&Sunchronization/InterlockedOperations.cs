using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    public class BankAccount2
    {
        private int balance;                    //(Backing Field)This is required to get a ref to it (Because Interlocked.Add() expects a reference)

        public int Balance { get => balance; set => balance = value; }

        public BankAccount2()
        {
            Balance = 0;
        }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);                   //Interlocked class provides synchronization for primitive types
        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
            //Interlocked.MemoryBarrier();   used to maintain order of execution of statements written before and after memory barrier
            //Interlocked.Exchange();               //Thread safe assignments
            //Interlocked.CompareExchange();        //Thread safe comparisons - compares 2 values for equality, if equal replaces 1st value
        }
    }

    internal class InterlockedOperations
    {
        public static void Run()
        {
            var ba = new BankAccount2();

            var tasks = new List<Task>();

            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
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
