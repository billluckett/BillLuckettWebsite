using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL19.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BL19.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _emailSender;

        public HomeController(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View("Home");
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Resume()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // POST: Home/Contact
        [HttpPost]
        public async Task<ActionResult> Contact(ContactViewModel vm)
        {
            var toAddress = "luckett.bill@gmail.com";
            var subject = "Email from billluckett.com!";
            var sender = String.IsNullOrEmpty(vm.Email) ? "UNSPECIFIED_SENDER" : vm.Email;
            var message = string.Format("<p>From: {0}</p><p>Subject: Contact form submit</p><p>Message:</p><p>{1}</p>", sender, vm.Comments);

            try
            {
                await _emailSender.SendEmailAsync(toAddress, subject, message);
            }
            catch (Exception e)
            {
                ViewData["Text"] = "Exception in Post: Home/Contact. Message: " + e.Message;
                return View("Ex");
            }

            return RedirectToAction("ContactThanks");
        }

        public IActionResult ContactThanks()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
