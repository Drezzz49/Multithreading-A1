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
            this.balance = initalBalance;
        }

        public void Deposit(double amount)
        {
            //medvetet inget lås
            balance += amount;
        }

        public void Withdraw(double amount)
        {
            //medvetet inget lås
            balance -= amount;
        }

        public double getBalance()
        {
            return balance;
        }
    }
}
