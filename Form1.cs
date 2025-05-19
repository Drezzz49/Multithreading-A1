using Multithreading_A1.Assignment_2;
using Multithreading_A1.Assignment_3;
using Multithreading_A1.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_A1
{
    public partial class Form1 : Form
    {
        private LoanTask loanTask;
        private ReturnTask returnTask;
        private AdminTask adminTask;
        private UpdateGUITask updateGUITask;

        private ProductManager productManager;
        private MemberManager memberManager;
        private LoanItemManager loanItemManager;

        private TransactionSimulator transactionSimulator;


        //assignment3
        private Storage storage;
        private List<Producer> producers;
        private List<Consumer> consumers;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            productManager = new ProductManager();
            memberManager = new MemberManager();
            loanItemManager = new LoanItemManager();

            // Fill test data
            productManager.GenerateTestProducts(25);//25 produkter
            memberManager.GenerateTestMembers(5);//5members som kan låna saker

            loanTask = new LoanTask(productManager, memberManager, loanItemManager);
            returnTask = new ReturnTask(productManager, loanItemManager);
            adminTask = new AdminTask(productManager);
            updateGUITask = new UpdateGUITask(productManager, loanItemManager, listBoxItems, listBoxLog);

            loanTask.Start();
            returnTask.Start();
            adminTask.Start();
            updateGUITask.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //? gör så jag kan kalla metoden "tryggt" även om den är null, det blir ingen NullReferenceException
            loanTask?.Stop();
            returnTask?.Stop();
            adminTask?.Stop();
            updateGUITask?.Stop();

        }

        private void btnAssignment2_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();

            transactionSimulator = new TransactionSimulator(1000, 3, 2000);
            transactionSimulator.Start();
            transactionSimulator.PrintResults(listBoxLog);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            transactionSimulator.Stop();
        }

        private void btnDeadlock_Click(object sender, EventArgs e)
        {
            DeadlockSimulator deadlockSimulator = new DeadlockSimulator(listBoxLog);
            deadlockSimulator.Start();
        }

        private void btnDeadlockFix_Click(object sender, EventArgs e)
        {
            DeadlockFixSimulator deadlockFixSimulator = new DeadlockFixSimulator(listBoxLog);
            deadlockFixSimulator.Start();
        }





        //assignment3 knapparna
        private void btnStartProducersConsumers_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
            storage = new Storage(10, listBoxLog); //new Storage with a buffer size of 10

            // Initialize producers
            producers = new List<Producer>
            {
                new Producer("Factory 1", storage),
                new Producer("Factory 2", storage),
                new Producer("Factory 3", storage)
            };

            // Initialize consumers with their respective maximum capacities
            consumers = new List<Consumer>
            {
                new Consumer("Store 1", storage, 12),
                new Consumer("Store 2", storage, 15),
                new Consumer("Store 3", storage, 10)
            };



            // Start all producer threads
            foreach (var producer in producers)
            {
                producer.Start();
            }
            // Start all consumer threads
            foreach (var consumer in consumers)
            {
                consumer.Start();
            }

            listBoxLog.Items.Add("Producers and Consumers started.");
        }

        private void btnStopProducersConsumers_Click(object sender, EventArgs e)
        {
            //? gör så jag kan kalla metoden "tryggt" även om den är null, det blir ingen NullReferenceException
            storage?.Stop();

            foreach (var producer in producers)
            {
                producer?.Stop();
            }

            foreach (var consumer in consumers)
            {
                consumer?.Stop();
            }

            // Clear the producer and consumer
            producers.Clear();
            consumers.Clear();
        }

        private void btnClearBuffer_Click(object sender, EventArgs e)
        {
            //? gör så jag kan kalla metoden "tryggt" även om den är null, det blir ingen NullReferenceException
            storage?.ClearBuffer();

            //reset production
            storage?.resetProductionCap();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
            listBoxItems.Items.Clear();
        }

    }
}
