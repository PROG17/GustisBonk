using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GustavsBANKS.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer()
        {
            Accounts = new List<Account>();
        }
    }
}
