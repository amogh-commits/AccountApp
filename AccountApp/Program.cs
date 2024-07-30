using System;
using AccountApp;
using AccountManagerSpace;
using AccountProject;
using AccountApp.Model;
using AccountLibrary;

namespace AccountProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            AccountManager.RoleSelection();
            SerialDeserial.SerializeData(AccountManager.accounts);
        }
    }
}
