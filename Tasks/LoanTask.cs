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


    }
}
