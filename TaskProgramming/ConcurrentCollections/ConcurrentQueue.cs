using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentQueue
    {
        public static void Run()
        {
            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
            concurrentQueue.Enqueue(1);
            concurrentQueue.Enqueue(2);

            int result;
            if (concurrentQueue.TryDequeue(out result))
            {
                Console.WriteLine($"Dequeued element: {result}");
            }

            if(concurrentQueue.TryPeek(out result))
            {
                Console.WriteLine($"Peeked element: {result}");
            }
        }
    }
}
