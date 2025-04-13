using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_A1.Tasks
{
    public class AdminTask
    {
        private readonly ProductManager productManager;
        private Thread thread;
        private volatile bool isRunning = false;
        private readonly Random random = new Random();

        private int productCounter = 1000; //for unqie id

        public AdminTask(ProductManager pm)
        {
            productManager = pm;
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
            while(isRunning)
            {
                try
                {
                    int delay = random.Next(6, 17);
                    Thread.Sleep(delay * 1000);

                    //75% att lägga till, 25% ta bort
                    if (random.NextDouble() < 0.75)
                    {
                        //lägger till en ny produkt
                        var newProduct = new Product(productCounter, $"Tool-Number {productCounter}");
                        productCounter++;
                        productManager.AddProduct(newProduct);
                        Console.WriteLine($"[AdminTask] Added {newProduct.Name}.");
                    }
                    else
                    {
                        //tar bort en gammal produkt
                        if (!productManager.isEmpty())
                        {
                            var product = productManager.GetRandomProduct();
                            productManager.RemoveProduct(product);
                            Console.WriteLine($"[AdminTask] Removed {product.Name} (lost or broken).");
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
