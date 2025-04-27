using System;
using System.Threading;
using System.Windows.Forms;

namespace Multithreading_A1.Assignment_2
{
    public class DeadlockSimulator
    {
        private BankAccountDeadlock accountA;
        private BankAccountDeadlock accountB;
        private ListBox listBoxLog;

        public DeadlockSimulator(ListBox listBoxLog)
        {
            this.listBoxLog = listBoxLog;
            //gör två konton
            accountA = new BankAccountDeadlock(1, 500, listBoxLog);
            accountB = new BankAccountDeadlock(2, 500, listBoxLog);
        }

        public void Start()
        {
            //två olika trådar som försöker gör transphers imellan varandra
            Thread thread1 = new Thread(() => accountA.Transfer(accountB, 100));
            Thread thread2 = new Thread(() => accountB.Transfer(accountA, 200));

            thread1.Start();
            thread2.Start();
        }
    }






    //bank-konto klass för att inte tar hänsyn till att man kan få deadlock
    public class BankAccountDeadlock
    {
        public int AccountId { get; }
        private double balance;
        private readonly object lock_ = new object();
        private ListBox listBoxLog;

        public BankAccountDeadlock(int id, double initialBalance, ListBox listBoxLog)
        {
            this.AccountId = id;
            this.balance = initialBalance;
            this.listBoxLog = listBoxLog;
        }

        public void Transfer(BankAccountDeadlock toAccount, double amount)
        {
            //låser sig själv
            lock (this.lock_)
            {
                //skriver ut vad som hänt
                Log($"[Account {AccountId}] Locked self, trying to lock Account {toAccount.AccountId}...");

                Thread.Sleep(100); //simulera delay (så att både hinner låsa sig själva)

                //försöker låsa kontot som den ska skicka till
                lock (toAccount.lock_)
                {
                    Log($"[Account {AccountId}] Locked Account {toAccount.AccountId}!");

                    this.balance -= amount;
                    toAccount.balance += amount;
                }
            }
        }

        //hjälpmetod för att det ska skrivs ut i vald listbox
        private void Log(string message)
        {
            if (listBoxLog.InvokeRequired)
            {
                listBoxLog.Invoke(new Action(() => listBoxLog.Items.Add(message)));
            }
            else
            {
                listBoxLog.Items.Add(message);
            }
        }
    }
}
