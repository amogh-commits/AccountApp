using System;
using AccountApp.ExceptionHandling;

namespace AccountLibrary.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountBankName { get; set; }
        public string AccountAadharNumber { get; set; }
        public double AccountBalance { get; set; }

        public const double MINIMUM_BALANCE = 500;
        public bool AccountActive { get; set; }

        public Account() { }

        public Account(int accountId, string accountName, string accountBankName)
        {


            AccountId = accountId;
            AccountName = accountName;
            AccountBankName = accountBankName;
            AccountBalance = MINIMUM_BALANCE;
            AccountActive = true;
        }

        public Account(int accountId, string accountName, string accountBankName, double accountBalance) : this(accountId, accountName, accountBankName)
        {
            if (accountBalance < MINIMUM_BALANCE)
            {
                AccountBalance = MINIMUM_BALANCE;
            }
            else
            {
                AccountBalance = accountBalance;
            }
        }

        public Account(int accountId, string accountName, string accountBankName, string accountAadharNumber) : this(accountId, accountName, accountBankName)
        {
            AccountAadharNumber = accountAadharNumber;
        }

        public Account(int accountId, string accountName, string accountBankName, string accountAadharNumber, double accountBalance) : this(accountId, accountName, accountBankName, accountAadharNumber)
        {
            if (accountBalance < MINIMUM_BALANCE)
            {
                AccountBalance = MINIMUM_BALANCE;
            }
            else
            {
                AccountBalance = accountBalance;
            }
        }

        public void Deposit(double depositAmount)
        {
            if (AccountActive)
            {
                AccountBalance += depositAmount;
                Console.WriteLine($"Deposited {depositAmount}. New balance: {AccountBalance}");
            }
            else
            {
                Console.WriteLine("Account is inactive.");
            }
        }

        public void Withdraw(double withdrawAmount)
        {
            if (AccountActive)
            {
                if (AccountBalance >= withdrawAmount)
                {
                    AccountBalance -= withdrawAmount;
                    Console.WriteLine($"Withdrew {withdrawAmount}. New balance: {AccountBalance}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("Account is inactive.");
            }
        }

        public void PrintAccountDetails()
        {
            if (AccountActive)
            {
                Console.WriteLine($"ID: {AccountId}");
                Console.WriteLine($"Name: {AccountName}");
                Console.WriteLine($"Bank Name: {AccountBankName}");
                Console.WriteLine($"Aadhar Number: {AccountAadharNumber}");
                Console.WriteLine($"Balance: {AccountBalance}");
                Console.WriteLine($"Active: {AccountActive}");
            }
            else
            {
                Console.WriteLine("This account is inactive.");
            }
        }
    }
}
