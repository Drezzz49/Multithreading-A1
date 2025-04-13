using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Tasks
{
    public class ReturnTask
    {
        private readonly ProductManager productManager;
        private readonly LoanItemManager loanItemManager;

        private Thread thread;
        private volatile bool isRunning = false;
        private readonly Random random = new Random();

        public ReturnTask(ProductManager pm, LoanItemManager lim)
        {
            this.productManager = pm;
            this.loanItemManager = lim;
        }

        public void Start()
        {
            isRunning = true;
            thread = new Thread(Run);
            thread.IsBackground = true; //"This thread isn’t essential — if the main program finishes, you can kill this thread automatically."
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void Run()
        {
            while (isRunning)
            {
                try
                {
                    int delay = random.Next(3, 16);
                    Thread.Sleep(delay * 1000);

                    //kollar så det finns utlånade saker
                    if (loanItemManager.isEmpty())
                        continue;

                    //hämtar ett slumpat föremål att lämna tillbaka
                    LoanItem loanItem = loanItemManager.GetRandomLoanItem();
                    if (loanItem == null) continue;

                    //Återlämna produkten och ta bort det från låneregistret
                    productManager.AddProduct(loanItem.LoanedProduct);
                    loanItemManager.RemoveLoanItem(loanItem);

                    Console.WriteLine($"[ReturnTask] {loanItem.LoanedProduct.Name} returned by {loanItem.Borrower.Name} after {delay} days.");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
