using System;
using Xunit;
using GustavsBANKS.Repo;
using GustavsBANKS.Models;
using System.Linq;
using System.Collections.Generic;

namespace bank.tests
{
    public class UnitTest1
    {
        [Fact]
        public void VerifyTransferWhenInsufficientFunds()
        {
            CreateCustommer();
            var transfer = new TransferVM
            {
                FromAccountNumber = 1002,
                ToAccountNumber = 1003,
                Ammount = 1001
            };

            BankRepository.TransferFunds(transfer);
            decimal expected = 1000;
            decimal actual = BankRepository.Customers.FirstOrDefault(x => x.CustomerId == 1002).Accounts.FirstOrDefault().Balance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VerifyTransfer()
        {
            CreateCustommer();
            var transfer = new TransferVM
            {
                FromAccountNumber = 1002,
                ToAccountNumber = 1003,
                Ammount = 1000
            };

            BankRepository.TransferFunds(transfer);
            decimal expected = 2000;
            decimal actual = BankRepository.Customers.FirstOrDefault(x => x.CustomerId == 1002).Accounts.FirstOrDefault().Balance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DepositTest()
        {
            CreateCustommer();
            var deposit = new DepositWithrawVM
            {
                AccountNumber = 1002,
                Ammount = 500
            };

            BankRepository.DepositFunds(deposit);

            decimal expectedValue = 1500;
            decimal actualValue = BankRepository.Customers.FirstOrDefault(x => x.Name == "Gustav Cleveman").Accounts.FirstOrDefault().Balance;

            Assert.Equal(expectedValue, actualValue);
        }


        [Fact]
        public void WithdrawalTest()
        {
            CreateCustommer();
            var withdrawal = new DepositWithrawVM
            {
                AccountNumber = 1002,
                Ammount = 500
            };

            BankRepository.WithdrawFunds(withdrawal);

            decimal expectedValue = 500;
            decimal actualValue = BankRepository.Customers.FirstOrDefault(x => x.Name == "Gustav Cleveman").Accounts.FirstOrDefault().Balance;

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void InsufficinetFundsTest()
        {
            CreateCustommer();
            var withdrawal = new DepositWithrawVM
            {
                AccountNumber = 1002,
                Ammount = 1001
            };

            BankRepository.WithdrawFunds(withdrawal);

            decimal expectedValue = 1000;
            decimal actualValue = BankRepository.Customers.FirstOrDefault(x => x.Name == "Gustav Cleveman").Accounts.FirstOrDefault().Balance;

            Assert.Equal(expectedValue, actualValue);
        }




        #region helpers
        private void CreateCustommer()
        {
            BankRepository.Customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1001,
                    Name = "Gustav Cleveman",
                    Accounts = CreateAccounts(1)
                },
                new Customer
                {
                    CustomerId = 1002,
                    Name = "Alexander Arvanitis",
                    Accounts = CreateAccounts(2)
                }
            };
        }

        private List<Account> CreateAccounts(int nbr)
        {
            var account = new Account
            {
                AccountNumber = 1001 + nbr,
                Balance = 1000
            };

            return new List<Account>
            {
                account
            };
        }
        #endregion
    }
}
