using System;
using System.Globalization;

namespace Questao1.Domain.Model;

public class Account
{
    public string AccountNumber { get; private set; }
    public string Holder { get; set; }
    private double Balance { get; set; }

    private const double Tax = 3.50;

    public Account(string accountNumber, string holder, double InitialBalance = 0)
    {
        AccountNumber = accountNumber;
        Holder = holder;
        Balance = InitialBalance;
    }

    public void Deposit(double value)
    {
        if (value > 0)
        {
            Balance += value;
        }
    }

    public void Withdraw(double value)
    {
        double ValueWithTax = value + Tax;
        if (value > 0 && Balance >= ValueWithTax)
        {
            Balance -= ValueWithTax;
        }
    }

    public string GetAccountInfo()
    {
        return $"Account {AccountNumber}, Holder: {Holder}, Balance: $ {Balance:F2}";
    }
}
