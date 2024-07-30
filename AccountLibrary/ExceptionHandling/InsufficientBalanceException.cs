using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.ExceptionHandling
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("Withdrawal failed. Account balance would fall below minimum balance.")
        {
        }
    }

}
