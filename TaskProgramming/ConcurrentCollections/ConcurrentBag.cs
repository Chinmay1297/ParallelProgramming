using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentBag
    {
        public static void Run()
        {
            //ConcurrentBag is a thread-safe, unordered collection of objects.
            //It is designed for scenarios where multiple threads need to add and remove items concurrently without the need for locking.
            //ConcurrentBag allows for fast and efficient addition and removal of items, making it suitable for scenarios where the order of items is not important.
            
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for(int i=0; i<10; i++)
            {
                int item = i; // Capture the current value of i
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(item);
                    Console.WriteLine($"{Task.CurrentId} has added {item}");
                    int result;
                    if(bag.TryPeek(out result))
                    {
                        Console.WriteLine($"{Task.CurrentId} peeked the value {result}");
                    }

                    //The thread adding the element, is peeking the same element here
                }));
            }

            Task.WaitAll(tasks.ToArray());

            int last = 0;
            if(bag.TryTake(out last))
            {
                Console.WriteLine("Last element taken from the bag: " + last);
            }
        }
    }
}
