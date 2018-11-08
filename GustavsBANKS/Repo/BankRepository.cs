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
    }
}
