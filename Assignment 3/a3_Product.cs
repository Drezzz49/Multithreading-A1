using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1.Assignment_3
{
    public class a3_Product
    {
        public string Name { get; }
        public double Price { get; }

        public a3_Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - {Price:C2}";
        }
    }
}
