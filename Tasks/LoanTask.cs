using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Tasks
{
    public class LoanTask
    {
        private readonly ProductManager productManager;
        private readonly MemberManager memberManager;
        private readonly LoanItemManager loanItemManager;

        private Thread thread;
        private volatile bool isRunning = false;
        private readonly Random random = new Random();


        public LoanTask(ProductManager pm, MemberManager mm, LoanItemManager lim)
        {
            this.productManager = pm;
            this.memberManager = mm;
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
                    int delay = random.Next(1, 4);
                    Thread.Sleep(delay * 1000);

                    //kollar om det finns någon produkt i lager som går att låna
                    if (productManager.isEmpty())
                        continue;

                    //hämtas en slumpmässig produkt från produktlistan
                    Product product = productManager.GetRandomProduct();
                    if (product == null) continue;

                    //hämtar en slumpmässig member i meberlistan
                    Member member = memberManager.GetRandomMember();
                    if (member == null) continue;

                    //lånar ut den till det slumpmässiga produkten till memben vid detta tillfället
                    LoanItem loanItem = new LoanItem(member, product, DateTime.Now);
                    
                    //lägger till det som loanitem och tar bort den produkten för den är utlånad nu
                    loanItemManager.AddLoanItem(loanItem);
                    productManager.RemoveProduct(product);

                    Console.WriteLine($"[LoanTask] {product.Name} loaned to {member.Name} for {delay} days.");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
