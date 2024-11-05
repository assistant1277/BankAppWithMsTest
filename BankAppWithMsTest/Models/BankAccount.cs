using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppWithMsTest.Models
{
    public class BankAccount
    {
        public BankAccount(double initialBalance)
        {
            Balance = initialBalance;
        }

        public double Balance { get; set; }
    }
}