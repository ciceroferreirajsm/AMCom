using Questao1.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao1.Services
{
    public class AccountBankService
    {
        public Account CreateAccount(string accountNumber, string accountHolder, double initialDeposit)
        {
            return new Account(accountNumber, accountHolder, initialDeposit);
        }

        public void Deposit(Account account, double amount)
        {
            account.Deposit(amount);
        }

        public void Withdraw(Account account, double amount)
        {
            account.Withdraw(amount);
        }

        public string GetAccountDetails(Account account)
        {
            return account.GetAccountInfo();
        }
    }

}
