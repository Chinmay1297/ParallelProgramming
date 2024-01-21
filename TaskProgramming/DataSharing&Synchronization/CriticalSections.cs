using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing_Sunchronization
{
    public class BankAccount
    {
        public int Balance { get; set; }

        public BankAccount()
        {
            Balance = 0;
        }

        public void Deposit(int amount)
        {
            //+= is not Atomic
            //op1: temp <- get_balance() + amt
            //op2: set_balance(temp)

            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    internal class CriticalSections
    {
        public static void Run()
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

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

            //Do you expect to get balance as 0, because you're depositing 1000 times and withdrawing 1000 times ?
            Console.WriteLine($"Final balance: ${ba.Balance}");             //The reason balance is not 0 is because withdraw and deposit are not atomic

            Console.ReadKey();
        }
    }
}
