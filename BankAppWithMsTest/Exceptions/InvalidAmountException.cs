using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppWithMsTest.Exceptions
{
    public class InvalidAmountException:Exception
    {
        public InvalidAmountException(string message) :base(message) { }
    }
}