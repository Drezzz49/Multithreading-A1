using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_A1.Assignment_2
{
    public class TransactionSimulator
    {
        private BankAccount bankAccount;
        private List<Client> clients;
        private List<Thread> threads;
        private int simulationTimeMilliseconds;

        public TransactionSimulator(double initalBalance, int numberOfClients, int simulationTimeMilliseconds)
        {
            bankAccount = new BankAccount(initalBalance);
            clients = new List<Client>();
            threads = new List<Thread>();
            this.simulationTimeMilliseconds = simulationTimeMilliseconds;

            for (int i = 0; i < numberOfClients; i++)
            {
                Client client = new Client(i, bankAccount); //lägger till samma clienter som har samma bank-konto
                clients.Add(client);

                Thread t = new Thread(client.Run); //lägger till trådar som kör för sig själva
                threads.Add(t);
            }
        }

        public void Start()
        {
            //starta alla trådar samtidigt
            foreach (var thread in threads)
            {
                thread.Start();
            }

            Thread.Sleep(simulationTimeMilliseconds);

            foreach (var client in clients)
            {
                client.Stop();
            }

            foreach (var thread in threads)
            {
                //wait until tread is done
                thread.Join();
            }
        }

        public void Stop()
        {
            foreach (var client in clients)
            {
                client.Stop();
            }

            foreach (var thread in threads)
            {
                //wait until tread is done
                thread.Join();
            }
        }


        public void PrintResults(ListBox listBox)
        {
            //Starta med initiala saldot (1000)
            //Lägg till alla insättningar och dra av alla uttag från klienterna
            //    (Summan av 'netAmount' för varje klient ger nettoförändringen)
            double expectedBalance = 1000 + clients.Sum(c => c.netAmount);

            // Långform (utan lambda):
            //double sum = 0;
            //foreach (Client c in clients)
            //{
            //    sum += c.netAmount;
            //}

            // Hämta det faktiska saldot från bankkontot
            double actualBalance = bankAccount.getBalance();
            // Beräkna skillnaden mellan förväntat och faktiskt saldo
            double discrepancy = Math.Abs(expectedBalance - actualBalance);

            listBox.Items.Add($"Expected Balance: {expectedBalance:F2}");
            listBox.Items.Add($"Actual Balance: {actualBalance:F2}");
            listBox.Items.Add($"Discrepancy: {discrepancy:F2}");

            //om balance skillnaden skiljer för mycket då har vi hittat en race-condition
            if (discrepancy > 0.01)
            {
                listBox.Items.Add("FAILURE: Race condition detected!");
            }
            else
            {
                listBox.Items.Add("SUCCESS: No race condition detected!");
            }
        }

        public BankAccount GetBankAccount()
        {
            return bankAccount;
        }

        public List<Client> GetClients()
        {
            return clients;
        }


    }
}
