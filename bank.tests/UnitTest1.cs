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
        public void DepositTest()
        {
            CreateCustommer();
            var deposit = new TransferAmmount
            {
                AccountNumber = 100120362,
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
            var withdrawal = new TransferAmmount
            {
                AccountNumber = 100120362,
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
            var withdrawal = new TransferAmmount
            {
                AccountNumber = 100120362,
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
                    Accounts = CreateAccounts()
                }
            };
        }

        private List<Account> CreateAccounts()
        {
            var account = new Account
            {
                AccountNumber = 100120362,
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
