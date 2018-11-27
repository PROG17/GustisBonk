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

        public static string TransferFunds(TransferVM transfer)
        {
            var fromAccount = FindAccount(transfer.FromAccountNumber);
            var toAccount = FindAccount(transfer.ToAccountNumber);
            if (fromAccount != null && toAccount != null)
            {
                if (transfer.Ammount <= fromAccount.Balance)
                {
                    fromAccount.Balance -= transfer.Ammount;
                    toAccount.Balance += transfer.Ammount;
                    return $"{transfer.Ammount}.kr överfört från konto {fromAccount.AccountNumber} till konto {toAccount.AccountNumber}. Aktuella Saldon är: {fromAccount.AccountNumber} : {fromAccount.Balance}.kr & {toAccount.AccountNumber} : {toAccount.Balance}.kr";
                }
                return "Ej tillräckligt på kontot.";
            }
            return "Något av kontona kunde inte hittas.";
        }

        public static string DepositFunds(DepositWithrawVM transfer)
        {
            var account = FindAccount(transfer.AccountNumber);
            if(account != null)
            {
                account.Balance += transfer.Ammount;
                return $"{transfer.Ammount}.kr insatta på konto {account.AccountNumber}. Aktuellt Saldo är: {account.Balance}.kr";
            }
            return "Kontot kunde inte hittas.";               
        }

        public static string WithdrawFunds(DepositWithrawVM transfer)
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
