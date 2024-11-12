using System;
using System.Collections.Generic;
class BankAccount
{
    public Guid AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    public BankAccount(string owner, decimal initialBalance)
    {
        AccountNumber = Guid.NewGuid();
        Owner = owner;
        Balance = initialBalance;
    }
    public void Deposit(decimal amount)
    {
        Balance += amount;
    }
    public bool Withdraw(decimal amount)
    {
        if (amount <= Balance)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }
    public override string ToString()
    {
        return $"Account Number: {AccountNumber}, Owner: {Owner}, Balance: {Balance}";
    }
}
class Program
{
    static void Main()
    {
        //Wird später mit Datenbank ersetzt
        List<BankAccount> accounts = new List<BankAccount>();
        while (true)
        {
            Console.WriteLine("Options:");
            Console.WriteLine("1. Show Accounts");
            Console.WriteLine("2. Create Account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Accounts:");
                    foreach (var account in accounts)
                    {
                        Console.WriteLine(account);
                    }
                    break;
                case "2":
                    Console.Write("Enter the owner's name: ");
                    var owner = Console.ReadLine();
                    Console.Write("Enter the initial balance: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
                    {
                        BankAccount newAccount = new BankAccount(owner, initialBalance);
                        accounts.Add(newAccount);
                        Console.WriteLine("New account created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid initial balance input.");
                    }
                    break;
                case "3":
                    Console.Write("Enter the account number to deposit into: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid depositAccountNumber))
                    {
                        var depositAccount = accounts.Find(account => account.AccountNumber == depositAccountNumber);
                        if (depositAccount != null)
                        {
                            Console.Write("Enter the deposit amount: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                            {
                                depositAccount.Deposit(depositAmount);
                                Console.WriteLine("Deposit successful.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid deposit amount input.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid account number input.");
                    }
                    break;
                case "4":
                    Console.Write("Enter the account number to withdraw from: ");
                    if (Guid.TryParse(Console.ReadLine(), out Guid withdrawAccountNumber))
                    {
                        var withdrawAccount = accounts.Find(account => account.AccountNumber == withdrawAccountNumber);
                        if (withdrawAccount != null)
                        {
                            Console.Write("Enter the withdrawal amount: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                            {
                                if (withdrawAccount.Withdraw(withdrawAmount))
                                {
                                    Console.WriteLine("Withdrawal successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid withdrawal amount input.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid account number input.");
                    }
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}