using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1
{
    public class MemberManager
    {
        private List<Member> members = new List<Member>();
        private readonly object _lock = new object();
        private int nextId = 1;

        public void addMember(string name)
        {
            lock (_lock)
            {
                members.Add(new Member(nextId++, name));
            }
        }

        public List<Member> GetAll()
        {
            lock (_lock)
            {
                return members.ToList();
            }
        }

        public bool isEmpty()
        {
            lock (_lock)
            {
                //true om members.count är 0 annars falsk
                return members.Count == 0;
            }
        }

        public Member GetRandomMember()
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
                    return members[rnd.Next(members.Count)];
                }
            }
        }

        public void GenerateTestMembers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                addMember($"House {i+1}");
            }
        }

    }
}
