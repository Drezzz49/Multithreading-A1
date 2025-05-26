using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_CS_GUI
{

    // Status for each buffer slot
    public enum BufferStatus
    {
        Empty,
        New,
        Checked 
    }


    public class BoundedBuffer
    {
        private string[] buffer;// Array to hold string lines
        private BufferStatus[] status;// Parallel array to track status of each slot
        private int size;// Total buffer capacity

        private object _lock = new object();

        public BoundedBuffer(int capacity)
        {
            size = capacity;
            buffer = new string[size];// Create buffer of given size
            status = new BufferStatus[size];// Create matching status array


            // Initialize each slot as empty
            for (int i = 0; i < size; i++)
            {
                buffer[i] = string.Empty;
                status[i] = BufferStatus.Empty;
            }
        }

        // Called by the first producer: writes raw data into a slot
        public void Write(string line, int index)
        {
            lock (_lock)
            {
                // Wait if the slot is not empty (still being used)
                while (status[index] != BufferStatus.Empty)
                {
                    Monitor.Wait(_lock);// Wait until someone else frees the slot
                }

                // Write the new data and update its status
                status[index] = BufferStatus.New;
                buffer[index] = line;

                // Wake up other waiting threads
                Monitor.PulseAll(_lock);
            }
        }


        // Called by a modifier thread: reads raw data for editing
        public string ReadForModify(int index)
        {
            lock (_lock)
            {
                // Wait if the slot doesn't have new data yet
                while (status[index] != BufferStatus.New)
                {
                    Monitor.Wait(_lock);// Wait until someone else frees the slot

                }
                // Return the original (unmodified) line
                return buffer[index];
            }
        }


        // Called by the modifier thread: writes the modified result back into the buffer
        public void WriteModified(string line, int index)
        {
            lock (_lock)
            {
                buffer[index] = line; // Save modified line
                status[index] = BufferStatus.Checked;// Mark as checked/validated
                Monitor.PulseAll(_lock); // Notify any waiting final reader
            }
        }

        // Called by the final consumer: reads the checked/validated data
        public string ReadChecked(int index)
        {
            lock (_lock)
            {
                // Wait until the modified result is available
                while (status[index] != BufferStatus.Checked)
                {
                    Monitor.Wait(_lock);
                }
                string line = buffer[index]; // Retrieve the checked line
                status[index] = BufferStatus.Empty;// Mark slot as empty again (ready for reuse)

                Monitor.PulseAll(_lock);// Wake up any waiting writers
                return line;
            }
        }

    }
}
