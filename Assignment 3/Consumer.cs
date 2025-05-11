using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Assignment_3
{
    public class Consumer
    {
        private string name;
        private Storage storage;
        private Thread thread;
        private volatile bool running;
        private int maxCapacity; // Maximum capacity the consumer can have
        private List<a3_Product> items;  // List of items currently being carried by this consumer

        public Consumer(string name, Storage storage, int maxCapacity)
        {
            this.name = name;
            this.storage = storage;
            this.maxCapacity = maxCapacity;
            this.items = new List<a3_Product>();
            this.thread = new Thread(Run);
        }

        public void Start()
        {
            running = true;
            thread.Start();// Start the thread
        }

        public void Stop()
        {
            running = false;
            thread.Join(); // Wait for the thread to finish
        }

        private void Run()
        {
            while (running)
            {
                if (items.Count < maxCapacity) //have room for more?
                {
                    a3_Product product = storage.Consume(); //consumes latest product
                    items.Add(product); //adds it to list of items carried by this consumer
                }
                else
                {
                    // Clear the truck if it's full, basically gicing the customer a new truck to load things in
                    items.Clear();
                }

                Thread.Sleep(1000); // Simulate processing time
            }
        }
    }
}
