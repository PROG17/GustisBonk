using GustavsBANKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GustavsBANKS.Repo
{
    public static class BankRepository
    {
        public static List<Customer> Customers { get; set; }

        static BankRepository()
        {
            Customers = new List<Customer>();
        }

        public static string DepositFunds(TransferAmmount transfer)
        {
            var account = FindAccount(transfer.AccountNumber);
            if(account != null)
            {
                account.Balance += transfer.Ammount;
                return $"{transfer.Ammount}.kr insatta på konto {account.AccountNumber}. Aktuellt Saldo är: {account.Balance}.kr";
            }
            return "Kontot kunde inte hittas.";               
        }

        public static string WithdrawFunds(TransferAmmount transfer)
        {
            var account = FindAccount(transfer.AccountNumber);
            if (account != null)
            {
                if (transfer.Ammount <= account.Balance)
                {
                    account.Balance -= transfer.Ammount;
                    return $"{transfer.Ammount}.kr uttaget från konto {account.AccountNumber}. Aktuellt Saldo är: {account.Balance}.kr";
                }
                return "Ej tillräckligt på kontot.";
            }
            return "Kontot kunde inte hittas.";

        }

        private static Account FindAccount(int accountNumber)
        {
            foreach(var customer in Customers)
            {
                foreach(var account in customer.Accounts)
                {
                    if(account.AccountNumber == accountNumber)
                    {
                        return account;
                    }
                }
            }
            return null;
        }

    }
}
