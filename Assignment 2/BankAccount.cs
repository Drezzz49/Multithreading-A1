using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1.Assignment_2
{
    public class BankAccount
    {
        private double balance;
        private readonly object lock_ = new object();


        public BankAccount(double initalBalance)
        {
            lock (lock_)
            {
                this.balance = initalBalance;
            }
        }

        // Det här är den kritiska sektionen utan synkronisering
        public void Deposit(double amount)
        {
            //medvetet inget lås
            //balance += amount;

            //treadsafe
            lock (lock_)
            {
                balance += amount;
            }
        }

        // Det här är den kritiska sektionen utan synkronisering
        public void Withdraw(double amount)
        {
            //medvetet inget lås
            //balance -= amount;

            //treadsafe
            lock (lock_)
            {
                balance -= amount;
            }
        }

        public double getBalance()
        {
            lock (lock_)
            {
                return balance;
            }
        }
    }
}
