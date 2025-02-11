using Questao1.Services;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var service = new AccountBankService();

        Console.Write("Enter account number: ");
        string accountNumber = Console.ReadLine();

        Console.Write("Enter account holder's name: ");
        string accountHolder = Console.ReadLine();

        Console.Write("Is there an initial deposit (y/n)? ");
        string initialDepositResponse = Console.ReadLine().ToLower();
        double initialDeposit = 0;

        if (initialDepositResponse == "y")
        {
            Console.Write("Enter the initial deposit amount: ");
            initialDeposit = double.Parse(Console.ReadLine());
        }

        var account = service.CreateAccount(accountNumber, accountHolder, initialDeposit);

        while (true)
        {
            Console.WriteLine("\nCurrent Account Details:");
            Console.WriteLine(service.GetAccountDetails(account));

            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1 - Deposit");
            Console.WriteLine("2 - Withdraw");
            Console.WriteLine("3 - View Account Details");
            Console.WriteLine("4 - Exit");

            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.Write("\nEnter deposit amount: ");
                double depositAmount = double.Parse(Console.ReadLine());
                service.Deposit(account, depositAmount);
                Console.WriteLine("\nAccount updated after deposit.");
            }
            else if (option == "2")
            {
                Console.Write("\nEnter withdrawal amount: ");
                double withdrawalAmount = double.Parse(Console.ReadLine());
                service.Withdraw(account, withdrawalAmount);
                Console.WriteLine("\nAccount updated after withdrawal.");
            }
            else if (option == "3")
            {
                Console.WriteLine("\nAccount Details:");
                Console.WriteLine(service.GetAccountDetails(account));
            }
            else if (option == "4")
            {
                Console.WriteLine("Exiting the program...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}
