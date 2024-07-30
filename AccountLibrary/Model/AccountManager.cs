using System;
using System.Collections.Generic;
using System.Security.Principal;
using AccountApp.ExceptionHandling;
using AccountApp.Model;
using AccountLibrary.Model;


namespace AccountManagerSpace
{
    public class AccountManager
    {
        public static List<Account> accounts = new List<Account>();

        public static void RoleSelection()
        {
            accounts = SerialDeserial.DeserializeData();
            Console.WriteLine("Welcome to the Bank Management System!");

            while (true)
            {
                Console.WriteLine("\nSelect login type:");
                Console.WriteLine("1. User");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int loginChoice = int.Parse(Console.ReadLine());

                switch (loginChoice)
                {
                    case 1:
                        UserMenu();
                        break;
                    case 2:
                        AdminMenu();
                        break;
                    case 3:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void UserMenu()
        {
            Console.WriteLine("\nUser Menu:");
            Console.WriteLine("1. Access Account");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");
            int userChoice = int.Parse(Console.ReadLine());

            switch (userChoice)
            {
                case 1:
                    AccessAccount();
                    break;
                case 2:
                    Console.WriteLine("Exiting User Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }


        public static void AccessAccount()
        {
            Console.WriteLine("Enter the Account Id you want to access: ");
            int userInputForAccountId;

            if (!int.TryParse(Console.ReadLine(), out userInputForAccountId))
            {
                Console.WriteLine("Invalid input format. Account ID should be a number.");
                return;
            }

            try
            {
                Account selectedAccount = FindAccountById(userInputForAccountId);

                if (selectedAccount != null)
                {
                    Console.WriteLine("Enter the action you want to perform. 1. Deposit 2. Withdraw 3. Check Details");
                    int userInputForAction;

                    if (!int.TryParse(Console.ReadLine(), out userInputForAction))
                    {
                        Console.WriteLine("Invalid input format. Action choice should be a number.");
                        return;
                    }

                    switch (userInputForAction)
                    {
                        case 1:
                            Console.WriteLine("Enter amount to Deposit: ");
                            double depositAmount;

                            if (!double.TryParse(Console.ReadLine(), out depositAmount))
                            {
                                Console.WriteLine("Invalid input format. Deposit amount should be a number.");
                                return;
                            }

                            selectedAccount.Deposit(depositAmount);
                            break;
                        case 2:
                            Console.WriteLine("Enter amount to Withdraw: ");
                            double withdrawAmount;

                            if (!double.TryParse(Console.ReadLine(), out withdrawAmount))
                            {
                                Console.WriteLine("Invalid input format. Withdraw amount should be a number.");
                                return;
                            }

                            selectedAccount.Withdraw(withdrawAmount);
                            break;
                        case 3:
                            selectedAccount.PrintAccountDetails();
                            break;
                        default:
                            Console.WriteLine("Invalid action selected.");
                            break;
                    }
                }
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid number.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add New Account");
                Console.WriteLine("2. Display All Accounts");
                Console.WriteLine("3. Find Account by ID");
                Console.WriteLine("4. Update Account");
                Console.WriteLine("5. Remove Account");
                Console.WriteLine("6. Clear All Accounts");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                int adminChoice = int.Parse(Console.ReadLine());

                switch (adminChoice)
                {
                    case 1:
                        AddNewAccount();
                        break;
                    case 2:
                        DisplayAllAccounts();
                        break;
                    case 3:
                        Console.WriteLine("Enter Account ID:");
                        int accountIdToFind = int.Parse(Console.ReadLine());
                        FindAccountById(accountIdToFind);
                        break;
                    case 4:
                        UpdateAccount();
                        break;
                    case 5:
                        RemoveAccount();
                        break;
                    case 6:
                        ClearAllAccounts();
                        break;
                    case 7:
                        Console.WriteLine("Exiting Admin Menu...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void AddNewAccount()
        {
            if (accounts == null)
            {
                accounts = new List<Account>();
            }
            Console.WriteLine("Enter Account ID:");
            int accountId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Account Name:");
            string accountName = Console.ReadLine();

            Console.WriteLine("Enter Bank Name:");
            string bankName = Console.ReadLine();

            Console.WriteLine("Enter Aadhar Number:");
            string aadharNumber = Console.ReadLine();

            Console.WriteLine("Enter Initial Balance:");
            double initialBalance = double.Parse(Console.ReadLine());

            Account newAccount = new Account(accountId, accountName, bankName, aadharNumber, initialBalance);
            accounts.Add(newAccount);

            Console.WriteLine("New account added successfully.");
        }


        public static void DisplayAllAccounts()
        {
            Console.WriteLine("\nAll Active Accounts:");
            foreach (var account in accounts)
            {
                if (account.AccountActive)
                {
                    account.PrintAccountDetails();
                    Console.WriteLine();
                }
            }
        }

        public static Account FindAccountById(int accountId)
        {
            Account foundAccount = accounts.Find(account => account.AccountId == accountId);

            if (foundAccount == null)
            {
                throw new AccountNotFoundException($"Account with ID not found.");
            }
            else if (foundAccount.AccountActive == false)
            {
                Console.WriteLine("Account is not active");
            }


            return foundAccount;
        }



        public static void UpdateAccount()
        {
            Console.WriteLine("Enter Account ID:");
            int accountId = int.Parse(Console.ReadLine());

            Account foundAccount = accounts.Find(account => account.AccountId == accountId);

            if (foundAccount != null)
            {
                if (foundAccount.AccountActive)
                {
                    Console.WriteLine("Enter new Account Name:");
                    foundAccount.AccountName = Console.ReadLine();

                    Console.WriteLine("Enter new Bank Name:");
                    foundAccount.AccountBankName = Console.ReadLine();

                    Console.WriteLine("Enter new Aadhar Number:");
                    foundAccount.AccountAadharNumber = Console.ReadLine();

                    Console.WriteLine("Enter new Balance:");
                    foundAccount.AccountBalance = double.Parse(Console.ReadLine());

                    Console.WriteLine("Account updated successfully.");
                }
                else
                {
                    Console.WriteLine("Cannot update an inactive account.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public static void RemoveAccount()
        {
            Console.WriteLine("Enter Account ID to remove:");
            int accountId = int.Parse(Console.ReadLine());

            Account accountToRemove = accounts.Find(account => account.AccountId == accountId);

            if (accountToRemove != null)
            {
                accountToRemove.AccountActive = false;
                Console.WriteLine("Account marked as inactive successfully.");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        public static void ClearAllAccounts()
        {
            accounts.Clear();
            Console.WriteLine("All accounts cleared successfully.");
        }
    }
}
