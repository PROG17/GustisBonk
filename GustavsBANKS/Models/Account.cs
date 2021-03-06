﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GustavsBANKS.Models
{
    public class Account
    {
        [Required]
        [Display(Name = "Kontonummer")]
        public int AccountNumber { get; set; }

        [Required]
        [Display(Name = "Summa")]
        public decimal Balance { get; set; }


    }
}
