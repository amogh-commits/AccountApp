using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.ExceptionHandling
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string message) : base("Account not found.")
        {
        }
    }
}
