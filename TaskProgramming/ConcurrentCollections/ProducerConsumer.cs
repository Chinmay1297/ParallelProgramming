using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class Producer_Consumer
    {
        //Blocking Collection is a wrapper around any IProducerConsumerCollection<T> that provides blocking and bounding capabilities.
        //It is designed to be used in producer-consumer scenarios, where one or more threads are producing items and adding them to the collection,
        //while one or more threads are consuming items and taking them from the collection.
        //Bounded capacity means that the collection can only hold a certain number of items at a time. If the collection is full, producers will block until space is available.

        static BlockingCollection<int> messages = new BlockingCollection<int>(new ConcurrentBag<int>(), 10);

        static CancellationTokenSource cts = new CancellationTokenSource();

        static Random random = new Random();

        public static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(ex =>
                {
                    if (ex is OperationCanceledException)
                    {
                        Console.WriteLine("Operation was cancelled.");
                        return true;
                    }
                    return false;
                });
            }
        }

        public static void Run()
        {
            Task.Factory.StartNew(ProduceAndConsume, cts.Token);
            Console.ReadKey();
            cts.Cancel();
        }

        private static void RunConsumer()
        {
            //GetConsumingEnumerable method returns an enumerable that removes and returns items from the collection until the collection is marked as complete for adding and is empty.
            foreach (var item in messages.GetConsumingEnumerable())
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"Consumed: {item}");
                Thread.Sleep(random.Next(1000));
            }
        }

        private static void RunProducer()
        {
            while(true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"Produced: {i}");
                Thread.Sleep(random.Next(1000));

            }
        }
    }
}
