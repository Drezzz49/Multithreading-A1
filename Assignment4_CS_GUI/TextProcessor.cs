using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_CS_GUI
{
    public class TextProcessor
    {
        private List<string> inputLines;// Original lines of text to process
        private BoundedBuffer buffer;// Shared buffer used between threads
        private string find, replace;// Strings used for find/replace operations
        private RichTextBox outputBox;// GUI element to display modified output
        private ListBox statusBox;// GUI element to log thread status

        private const int BUFFER_SIZE = 20;// Fixed size of buffer (20 slots)

        public TextProcessor(List<string> lines, string find, string replace, RichTextBox output, ListBox status)
        {
            this.inputLines = lines;
            this.find = find;
            this.replace = replace;
            this.outputBox = output;
            this.statusBox = status;
            this.buffer = new BoundedBuffer(BUFFER_SIZE); // Initialize buffer with fixed size
        }


        public void Start()
        {
            //new threads
            Thread writer = new Thread(WriterThread);
            Thread modifier = new Thread(ModifierThread);
            Thread reader = new Thread(ReaderThread);

            //start the threads
            writer.Start();
            modifier.Start();
            reader.Start();
        }

        // First thread: writes each line into the buffer
        private void WriterThread()
        {
            for (int i = 0; i < inputLines.Count; i++)
            {
                int index = i % 20;// Map each line to a circular buffer slot (cause of out buffercap)
                buffer.Write(inputLines[i], index);// Write line into buffer
                Log($"Writer wrote line {i} to buffer[{index}]");
            }
        }


        //reads new lines, modifies them, and writes them back
        private void ModifierThread()
        {
            for (int i = 0; i < inputLines.Count; i++)
            {
                int index = i % 20;//same circular buffer system as writer
                string original = buffer.ReadForModify(index);// Waits until line is written
                string modified = original.Replace(find, replace);// Replace target text
                buffer.WriteModified(modified, index);// Mark as modified
                Log($"Modifier replaced text in buffer[{index}]");
            }
        }

        //reads final modified text and adds it to output
        private void ReaderThread()
        {
            for (int i = 0; i < inputLines.Count; i++)
            {
                int index = i % 20;
                string line = buffer.ReadChecked(index); // Waits for checked/modified line
                AppendOutput(line);// Append to RichTextBox
                Log($"Reader read line from buffer[{index}]");
            }
        }


        // Thread-safe update to output text to the RichTextBox
        private void AppendOutput(string text)
        {
            // Checks if the current thread is not the UI thread
            if (outputBox.InvokeRequired)
            {
                // Invokes the action on the UI thread to safely append text to the outputBox
                outputBox.Invoke(new Action(() => outputBox.AppendText(text + "\n")));
            }
            else
            {
                // If already on the UI thread, appends the text directly to the outputBox
                outputBox.AppendText(text + "\n");
            }
        }

        private void Log(string message)
        {
            // Checks if the current thread is not the UI thread
            if (statusBox.InvokeRequired)
            {
                // Invokes the action on the UI thread to safely add the message to the statusBox's items
                statusBox.Invoke(new Action(() => statusBox.Items.Add(message)));
            }
            else
            {
                // If already on the UI thread, directly adds the message to the statusBox's items
                statusBox.Items.Add(message);
            }
        }

    }
}
