using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1
{
    public class ProductManager
    {
        private List<Product> products = new List<Product>();
        private readonly object _lock = new object();
        private int nextId = 100;

        public void AddProduct(string name)
        {
            lock (_lock)
            {
                products.Add(new Product(nextId++, name));
            }
        }

        //if you've already created a product and want to add it, the mothd above generates a id also
        public void AddProduct(Product product)
        {
            lock (_lock)
            {
                products.Add(product);
            }
        }

        public void RemoveProduct(Product product)
        {
            lock (_lock)
            {
                products.Remove(product);
            }
        }

        //getall hämtar bara all data från listan utan att ta själva listan, så den gör basically en kopia av den
        public List<Product> GetAll()
        {
            lock (_lock)
            {
                return products.ToList();
            }
        }

        
        public bool isEmpty()
        {
            lock(_lock)
            {
                //true om products.count är 0 annars falsk
                return products.Count == 0;
            }
        }

        public Product GetRandomProduct()
        {
            lock (_lock)
            {
                if (isEmpty())
                {
                    return null;
                }
                else
                {
                    Random rnd = new Random();
                    return products[rnd.Next(products.Count)];
                }
            }
        }

        public void GenerateTestProducts(int count)
        {
            string[] testNames = {
                "chair", "table", "mousepad", "mouse", "keyboard", "headphones", "monitor", "dehumidifier",
                "coffee cup", "blanket", "jacket", "pants", "shirt", "sunhat"
            };

            for (int i = 0; i < count; i++)
            {
                //om mängden av produkter vi vill göra är fler är listan med produkter ovan så loopar den och återanvänder
                AddProduct(testNames[i % testNames.Length]);
            }
        }

    }
}
