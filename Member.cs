using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Member(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //skriver över tostring funktionen för member så man ser dess id och namn
        public override string ToString()
        {
            return $"Member [ID: {Id}, Name: {Name}]";
        }
    }
}
