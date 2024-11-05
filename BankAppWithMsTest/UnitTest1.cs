using BankAppWithMsTest.Exceptions;
using BankAppWithMsTest.Models;
using BankAppWithMsTest.Services;

namespace BankAppWithMsTest
{
    [TestClass]
    public class BankAccountServiceTests
    {   
        private IBankAccountService _service;

        //set up before each test
        [TestInitialize]
        public void Initialize()
        {
            //initialize bank account service for each test
            _service = new BankAccountService();
        }

        //test deposit with valid amounts to check if balance increase correctly
        [TestMethod]
        [DataRow(5,5)] //deposit 5 should result in balance 5 
        [DataRow(20,20)] //deposit 20 should result in balance 20
        [DataRow(0.01,0.01)]  
        public void Deposit_ValidAmount_IncreasesBalance(double amount,double expectedBalance)
        {
            //create new bank account with starting balance of 0
            var account=new BankAccount(0);

            //call deposit method to add amount to account
            _service.Deposit(account, amount);

            //check if account balance match expected balance
            Assert.AreEqual(expectedBalance, _service.GetBalance(account));
        }
        

        //test deposit with invalid amounts negative or 0 to ensure exception are thrown
        [TestMethod]
        [DataRow(-5)] //deposit of -5 should throw exception
        [DataRow(0)]     
        public void Deposit_InvalidAmount_ThrowsInvalidAmountException(double amount)
        {
            //create new bank account with starting balance of 0
            var account =new BankAccount(0);

            //check if deposit throws exception for invalid amount
            //Assert.ThrowsException<InvalidAmountException>() ->that specifically checks if code inside it throw exception of type InvalidAmountException
            //()=> _service.Deposit(account,amount) ->means this is lambda expression that represent code we are testing and in this case deposit method call
            //and if this code throw InvalidAmountException test will pass and if it does not or if it throw different exception test will fail
            Assert.ThrowsException<InvalidAmountException>(()=> _service.Deposit(account, amount));
        }

        //test withdraw with valid amount to check if balance decrease correctly
        [TestMethod]
        [DataRow(5,5)] //withdrawing 5 from 10 should leave 5    
        [DataRow(10,0)]//withdrawing 10 from 10 should leave 0 
        [DataRow(1,9)]        
        public void Withdraw_ValidAmount_DecreasesBalance(double amount, double expectedBalance)
        {
            //create new bank account with starting balance of 10
            var account= new BankAccount(10);

            _service.Withdraw(account, amount);

            //check if account balance matches expected balance
            Assert.AreEqual(expectedBalance, _service.GetBalance(account));
        }

        //test withdraw with invalid amounts negative or 0 to ensure exception are thrown
        [TestMethod]
        [DataRow(-5)]//withdraw of -5 should throw exception
        [DataRow(0)]     
        public void Withdraw_InvalidAmount_ThrowsInvalidAmountException(double amount)
        {
            //create new bank account with starting balance of 10
            var account= new BankAccount(10);

            //check if withdraw throw exception for invalid amount
            Assert.ThrowsException<InvalidAmountException>(()=> _service.Withdraw(account, amount));
        }

        //test withdraw that exceed balance to ensure insufficient balance exception is thrown
        [TestMethod]
        [DataRow(15)] //withdrawing 15 from 10 should throw exception
        [DataRow(100)]//withdrawing 100 from 10 should throw exception
        public void Withdraw_InsufficientBalance_ThrowsInsufficientBalanceException(double amount)
        {
            //create new bank account with starting balance of 10
            var account= new BankAccount(10);

            //check if withdraw throw exception when balance is insufficient
            Assert.ThrowsException<InsufficientBalanceException>(()=> _service.Withdraw(account, amount));
        }

        //test valid transfer from one account to another checking correct balance update
        [TestMethod]
        [DataRow(5,5,5)] //transferring 5 should leave 5 in account a and add 5 to account b 
        [DataRow(10,0,10)] //transferring 10 should empty account a and add 10 to account b
        public void Transfer_ValidAccount_TransfersAmount(double transferAmount,double expectedBalanceA,double expectedBalanceB)
        {
            //create two bank accounts-> a with 10 balance and b with 0 balance
            var accountA =new BankAccount(10);  
            var accountB =new BankAccount(0);

            //transfer specified amount from account a to account b
            _service.Transfer(accountA,accountB,transferAmount);

            //check if account a balance match expected balance after transfer
            Assert.AreEqual(expectedBalanceA, _service.GetBalance(accountA));

            //check if account b balance match expected balance after transfer
            Assert.AreEqual(expectedBalanceB, _service.GetBalance(accountB));
        }

        //test transfer to invalid account to ensure exception is thrown
        [TestMethod]
        [DataRow(null,5)] //transfer to null account should throw exception
        public void Transfer_InvalidAccount_ThrowsInvalidAccountException(BankAccount toAccount,double amount)
        {
            var fromAccount= new BankAccount(10);

            Assert.ThrowsException<InvalidAccountException>(()=> _service.Transfer(fromAccount,toAccount,amount));
        }

        //test transfer with insufficient fund to ensure exception is thrown
        [TestMethod]
        [DataRow(15)] //transferring 15 from account with 10 should throw exception  
        public void Transfer_InsufficientBalance_ThrowsInsufficientBalanceException(double amount)
        {
            //create two bank accounts-> a with 10 balance and b with 0 balance
            var accountA= new BankAccount(10);  
            var accountB =new BankAccount(0);

            //check if transfer throw exception when account has insufficient balance
            Assert.ThrowsException<InsufficientBalanceException>(()=> _service.Transfer(accountA,accountB,amount));
        }
    }
}