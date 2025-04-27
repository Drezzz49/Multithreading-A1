using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Assignment_2
{
    public class Client
    {
        private static Random random = new Random();
        public int ClientId { get; }
        public double netAmount { get; private set; } //har koll på hur mycket pengar det borde finnas
        private BankAccount bankAccount;
        private volatile bool running = true;



        public Client(int clientId, BankAccount account)
        {
            this.ClientId = clientId;   
            this.bankAccount = account;
        }

        public void Run()
        {
            while (running)
            {
                double amount = random.Next(1, 100);
                if(random.NextDouble() < 0.5)//50 50 chans att lägga till/ta ut pengar
                {
                    bankAccount.Deposit(amount);
                    netAmount += amount;
                }
                else
                {
                    bankAccount.Withdraw(amount);
                    netAmount -= amount;
                }
                Thread.Sleep(1);
            }
        }

        public void Stop()
        {
            running = false;
        }
    }
}
