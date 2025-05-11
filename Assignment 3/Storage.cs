using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_A1.Assignment_3
{
    public class Storage
    {
        private a3_Product[] buffer; //array for storing products
        private int itemCount; //number of items in the buffer
        private SemaphoreSlim empty; //semaphore for empty slots in the buffer
        private SemaphoreSlim full; //semaphore for full slots in the buffer
        private Mutex mutex;  // Mutex to enforce exclusive access to the buffer
        private ListBox listBoxLog; // Reference to the listBoxLog for UI updates
        private bool running; // Flag to control whether the storage is running or not

        private static int totalProducedItems = 0; // Static to track across all producers
        private static int maxProducedItems = 50; // Limit of total produced items

        public Storage(int bufferSize, ListBox listBoxLog)
        {
            this.buffer = new a3_Product[bufferSize];
            this.itemCount = 0;
            this.empty = new SemaphoreSlim(bufferSize); // Correct buffer size
            this.full = new SemaphoreSlim(0);
            this.mutex = new Mutex();
            this.listBoxLog = listBoxLog;
            this.running = true;
        }

        public void Stop()
        {
            running = false;

            // Release any blocked threads to let them exit
            empty.Release(buffer.Length);
            full.Release(buffer.Length);
        }

        public void Produce(a3_Product product)
        {
            if (!running || totalProducedItems >= maxProducedItems) return;

            // Wait for an empty slot before adding a product
            empty.Wait();
            // Lock the buffer to prevent race conditions
            mutex.WaitOne();

            if (running && totalProducedItems < maxProducedItems)
            {
                // Add the product to the next available slot in the buffer
                buffer[itemCount] = product;
                itemCount++;
                totalProducedItems++;

                listBoxLog.Invoke((MethodInvoker)delegate // Update UI from the main thread
                {
                    listBoxLog.Items.Add($"[Produced]: {product}");
                });
            }

            mutex.ReleaseMutex(); // Release the buffer lock
            full.Release(); // Signal that a new product is available, increasing the filled slot count
        }

        public a3_Product Consume()
        {
            if (!running) return null;

            full.Wait(); // Wait for a "filled" slot before consuming a product
            mutex.WaitOne(); //Lock the buffer to prevent race conditions

            a3_Product product = null;
            if (running && itemCount > 0)
            {
                product = buffer[itemCount - 1];// Get the last produced product
                buffer[itemCount - 1] = null;// Clear the slot in the buffer
                itemCount--;

                listBoxLog.Invoke((MethodInvoker)delegate
                {
                    listBoxLog.Items.Add($"[Consumed]: {product}");
                });
            }

            mutex.ReleaseMutex();// Release the buffer lock
            empty.Release(); // Signal that a slot has been emptied, increasing the empty slot count

            return product;
        }

        public void ClearBuffer()// empying the storage
        {
            mutex.WaitOne(); // Lock the buffer

            Array.Clear(buffer, 0, buffer.Length);// Clear the buffer
            itemCount = 0;

            // Release all semaphores to reset the state
            empty.Release(buffer.Length); // Make all slots available again
            full = new SemaphoreSlim(0); // No items in the buffer

            // Update UI from main thread
            listBoxLog.Invoke((MethodInvoker)delegate {
                listBoxLog.Items.Add("Buffer cleared");});

            mutex.ReleaseMutex();// Release the buffer lock
        }

        public void resetProductionCap()
        {
            mutex.WaitOne(); // Lock the buffer

            totalProducedItems = 0; // Reset the total produced items
            maxProducedItems = 50; // Reset the maximum produced items
            itemCount = 0; // Reset the item count

            mutex.ReleaseMutex(); // Release the buffer lock
        }
    }
}
