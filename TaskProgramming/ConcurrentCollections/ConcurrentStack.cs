using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentStack
    {
        public static void Run()
        {
            ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();
            concurrentStack.Push(1);
            concurrentStack.Push(2);
            concurrentStack.Push(3);
            concurrentStack.Push(3);
            int result;
            if (concurrentStack.TryPop(out result))
            {
                Console.WriteLine($"Popped element: {result}");
            }
            if (concurrentStack.TryPeek(out result))
            {
                Console.WriteLine($"Peeked element: {result}");
            }

            //You can also pop a range of elements from the stack using the TryPopRange method, which allows you to specify the number of elements to pop and returns them in an array.
            //TryPopRange method returns the number of elements that were successfully popped from the stack, and the popped elements are stored in the provided array.
            //This can be useful when you want to pop multiple elements at once without having to call TryPop multiple times.
            var results = new int[5];
            if(concurrentStack.TryPopRange(results, 0, 5) > 0)
            {
                Console.WriteLine($"Popped range of elements: {string.Join(", ", results)}");
            }
        }
    }
}
