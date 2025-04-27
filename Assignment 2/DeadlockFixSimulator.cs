using System;
using System.Threading;
using System.Windows.Forms;

namespace Multithreading_A1.Assignment_2
{
    public class DeadlockFixSimulator
    {
        private BankAccountNoDeadlock accountA;
        private BankAccountNoDeadlock accountB;
        private ListBox listBoxLog;

        public DeadlockFixSimulator(ListBox listBoxLog)
        {
            this.listBoxLog = listBoxLog;
            //gör två konton
            accountA = new BankAccountNoDeadlock(1, 500, listBoxLog);
            accountB = new BankAccountNoDeadlock(2, 500, listBoxLog);
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



    //bank-konto klass för att inte få deadlock
    public class BankAccountNoDeadlock
    {
        public int AccountId { get; }
        private double balance;
        private readonly object lock_ = new object();
        private ListBox listBoxLog;

        public BankAccountNoDeadlock(int id, double initialBalance, ListBox listBoxLog)
        {
            this.AccountId = id;
            this.balance = initialBalance;
            this.listBoxLog = listBoxLog;
        }

        public void Transfer(BankAccountNoDeadlock toAccount, double amount)
        {
            //defineras två variabler av typen BankAccountNoDeadlock, dvs två instanser av klassen
            BankAccountNoDeadlock firstLock, secondLock;

            //väljer vilket konto som får låset först baserat på deras kontoId
            if (this.AccountId < toAccount.AccountId)
            {
                //beroende på vilket konto som ska göra transaktionen så sätts firstlock och secondlock variablerna
                //till de olika kontona
                firstLock = this; 
                secondLock = toAccount; 
            }
            else
            {
                firstLock = toAccount;
                secondLock = this; 
            }

            //låster de kontot som har minst id och då blivit satt som firstlock där uppe
            lock (firstLock.lock_)
            {
                Log($"[Account {AccountId}] Locked {firstLock.AccountId}, trying to lock {secondLock.AccountId}...");
                Thread.Sleep(100);

                lock (secondLock.lock_)
                {
                    Log($"[Account {AccountId}] Locked {secondLock.AccountId}!");

                    this.balance -= amount;
                    toAccount.balance += amount;
                }
            }
        }

        //hjälpmetod för att skriva ut i vald listbox
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
