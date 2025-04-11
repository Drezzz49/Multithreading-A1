using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_A1
{
    public class LoanItem
    {
        public Member Borrower {  get; set; }
        public Product LoanedProduct { get; set; }
        public DateTime LoanedAt { get; set; }



        public LoanItem(Member borrower, Product loanedProduct)
        {
            Borrower = borrower;
            LoanedProduct = loanedProduct;
        }

        public override string ToString()
        {
            return $"LoanItem [Member: {Borrower.Name}, Product: {LoanedProduct.Name}, Loaned At: {LoanedAt} ]";
        }
    }
}
