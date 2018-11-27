using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GustavsBANKS.Models;
using GustavsBANKS.Repo;


namespace GustavsBANKS.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            List<Customer> model = BankRepository.Customers;

            return View(model);
        }

        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(TransferVM obj)
        {
            ViewBag.Message = BankRepository.TransferFunds(obj);

            return View();
        }

        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Deposit(DepositWithrawVM obj, string deposit, string withdrawal)
        {
            if (!string.IsNullOrEmpty(deposit))
            {
                ViewBag.Message = BankRepository.DepositFunds(obj);
            }
            if (!string.IsNullOrEmpty(withdrawal))
            {
                ViewBag.Message = BankRepository.WithdrawFunds(obj);
            }

            return View();
        }


        public IActionResult DepositFunds()
        {
            return RedirectToAction("Deposit", new { message = "test Message" });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
