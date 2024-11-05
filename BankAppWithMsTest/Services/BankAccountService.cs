using BankAppWithMsTest.Exceptions;
using BankAppWithMsTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppWithMsTest.Services
{
    public class BankAccountService:IBankAccountService
    {
        public void Deposit(BankAccount account,double amount)
        {
            if (amount<= 0)
                throw new InvalidAmountException("Invalid deposit amount");

            account.Balance+= amount;
        }

        public void Withdraw(BankAccount account, double amount)
        {
            if (amount<= 0)
                throw new InvalidAmountException("Invalid withdraw amount");

            if (account.Balance< amount)
                throw new InsufficientBalanceException("Insufficient balance");

            account.Balance-= amount;
        }

        public double GetBalance(BankAccount account)
        {
            return account.Balance;
        }

        public void Transfer(BankAccount fromAccount,BankAccount toAccount,double amount)
        {
            if (fromAccount==null || toAccount==null)
                throw new InvalidAccountException("Invalid account");

            Withdraw(fromAccount,amount);
            Deposit(toAccount,amount);
        }
    }
}