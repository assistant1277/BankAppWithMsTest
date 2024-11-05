using BankAppWithMsTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppWithMsTest.Services
{
    public interface IBankAccountService
    {
        void Deposit(BankAccount account,double amount);
        void Withdraw(BankAccount account,double amount);
        double GetBalance(BankAccount account);
        void Transfer(BankAccount fromAccount,BankAccount toAccount,double amount);
    }
}