using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Assignment_3
{
    public class Producer
    {
        private string name;
        private Storage storage;
        private Thread thread;
        private volatile bool running;
        private Random random = new Random();

        public Producer(string name, Storage storage)
        {
            this.name = name;
            this.storage = storage;
            this.thread = new Thread(Run);// Create a new thread that runs the 'Run' method
        }

        public void Start()
        {
            running = true;
            thread.Start();// Start the thread
        }

        public void Stop()
        {
            running = false;
            thread.Join(); // Wait for thread to finish
        }



        private void Run() // Main loop for the producer thread
        {
            while (running)
            {
                // Create a random product
                var product = new a3_Product($"Item-{random.Next(1000)}", random.Next(10, 100));
                storage.Produce(product);
                Thread.Sleep(random.Next(750, 1500)); // Random delay to simulate production
            }
        }
    }

}
