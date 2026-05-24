using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentCollections
{
    internal class ConcurrentDictionary
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? ("Task" + Task.CurrentId.Value) : "Main thread";
            Console.WriteLine($"{who} {(success ? "added" : "did not add")} the element");
        }
        public static void Run()
        {
            Task.Run(() => AddParis());
            AddParis();

            capitals["Russia"] = "Moscow"; //This is thread safe, but it will overwrite the value if key is already present. So, it is not recommended to use this indexer for adding/updating values in concurrent scenarios.

            //If key is not present, adds the key with the specified value. If key is present, updates the key with the result of the specified function.
            capitals.AddOrUpdate("Russia", "Leningrad", (key, oldValue) =>  oldValue + " --> " + "Leningrad");          

            Console.WriteLine($"Capital of Russia is {capitals["Russia"]}");

            var SwedensCapital = capitals.GetOrAdd("Sweden", "Stockholm"); //If key is not present, adds the key with the specified value. If key is present, returns the existing value.
            Console.WriteLine($"Capital of Sweden is {capitals["Sweden"]}");

            var SwedensCapital2 = capitals.GetOrAdd("Sweden", "XYZ"); 
            Console.WriteLine($"Capital of Sweden is {capitals["Sweden"]}");

            const string toRemove = "Russia";
            string removedCapital;
            bool didRemove = capitals.TryRemove(toRemove, out removedCapital); //Removes the key and value from the dictionary. Returns true if the key was successfully removed, otherwise false.
            if (didRemove)
            {
                Console.WriteLine("Removed {0} with capital {1}", toRemove, removedCapital);
            }
            else
            {
                Console.WriteLine("Failed to remove {0}", toRemove);
            }

            //Count or Empty check are expensive operations, because they need to iterate through the entire collection to count the number of elements. So, it is not recommended to use these properties in concurrent scenarios.
            //As multiple threads are accessing it, the count can change between the time you check it and the time you use it
            Console.WriteLine($"Count: {capitals.Count}, IsEmpty: {capitals.IsEmpty}");

            //Iterating through the dictionary is also an expensive operation
            foreach (var kv in capitals)
            {
                Console.WriteLine($"- {kv.Key} --> {kv.Value}");
            }
        }
    }
}
