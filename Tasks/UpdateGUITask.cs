using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_A1.Tasks
{
    public class UpdateGUITask
    {
        private readonly ProductManager productManager;
        private readonly LoanItemManager loanItemManager;
        private readonly ListBox listBoxItems;
        private readonly ListBox listBoxLog;

        private Thread thread;
        private volatile bool isRunning = false;

        public UpdateGUITask(ProductManager pm, LoanItemManager lim, ListBox itemsList, ListBox logList)
        {
            productManager = pm;
            loanItemManager = lim;
            listBoxItems = itemsList;
            listBoxLog = logList;
        }

        public void Start()
        {
            isRunning = true;
            thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void Run()
        {
            while (isRunning)
            {
                Thread.Sleep(1000);

                UpdateListBoxes();
            }
        }

        private void UpdateListBoxes()
        {
            //Det blir konstikt om fel tråd uppdaterar GUI, så om en annan tråd använder invoke så kommer 
            //denna blå true och då uppdatera listboxesen
            if (listBoxItems.InvokeRequired || listBoxLog.InvokeRequired)
            {
                listBoxItems.Invoke(new Action(UpdateListBoxes));
                return;
            }

            //clear
            listBoxItems.Items.Clear();
            foreach (var product in productManager.GetAll())
            {
                listBoxItems.Items.Add(product.ToString());
            }

            //clear
            listBoxLog.Items.Clear();
            foreach (var loan in loanItemManager.GetAll())
            {
                listBoxLog.Items.Add(loan.ToString());
            }
        }
    }
}
