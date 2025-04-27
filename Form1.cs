using Multithreading_A1.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_A1
{
    public partial class Form1 : Form
    {
        private LoanTask loanTask;
        private ReturnTask returnTask;
        private AdminTask adminTask;
        private UpdateGUITask updateGUITask;

        private ProductManager productManager;
        private MemberManager memberManager;
        private LoanItemManager loanItemManager;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            productManager = new ProductManager();
            memberManager = new MemberManager();
            loanItemManager = new LoanItemManager();

            // Fill test data
            productManager.GenerateTestProducts(25);//25 produkter
            memberManager.GenerateTestMembers(5);//5members som kan låna saker

            loanTask = new LoanTask(productManager, memberManager, loanItemManager);
            returnTask = new ReturnTask(productManager, loanItemManager);
            adminTask = new AdminTask(productManager);
            updateGUITask = new UpdateGUITask(productManager, loanItemManager, listBoxItems, listBoxLog);

            loanTask.Start();
            returnTask.Start();
            adminTask.Start();
            updateGUITask.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //? gör så jag kan kalla metoden "tryggt" även om den är null, det blir ingen NullReferenceException
            loanTask?.Stop();
            returnTask?.Stop();
            adminTask?.Stop();
            updateGUITask?.Stop();
        }

        private void btnAssignment2_Click(object sender, EventArgs e)
        {

        }
    }
}
