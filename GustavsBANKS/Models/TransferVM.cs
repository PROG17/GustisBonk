using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GustavsBANKS.Models
{
    public class TransferVM
    {
        [Required(ErrorMessage = "var god ange kontonummer!")]
        public int FromAccountNumber { get; set; }

        [Required(ErrorMessage = "var god ange kontonummer!")]
        public int ToAccountNumber { get; set; }

        [Range(0.1, 1000000000000000)]
        [Required(ErrorMessage = "var god ange summa!")]
        public decimal Ammount { get; set; }

    }
}
