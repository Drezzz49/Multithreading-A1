using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1
{
    public class LoanItemManager
    {
        private List<LoanItem> loanItems = new List<LoanItem>();
        private readonly object _lock = new object();

        public void AddLoanItem(LoanItem item)
        {
            lock (_lock)
            {
                loanItems.Add(item);
            }
        }

        public void RemoveLoanItem(LoanItem item)
        {
            lock (_lock)
            {
                loanItems.Remove(item);
            }
        }

        public List<LoanItem>GetAll()
        {
            lock (_lock)
            {
                return loanItems.ToList();
            }
        }

        public bool isEmpty()
        {
            lock(_lock)
            {
                return loanItems.Count == 0;
            }
        }

        public LoanItem GetRandomLoanItem()
        {
            lock (_lock)
            {
                if(isEmpty())
                {
                    return null;
                }
                else
                {
                    Random rnd = new Random();
                    return loanItems[rnd.Next(loanItems.Count)];
                }
            }
        }
    }
}
