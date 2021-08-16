using Microsoft.AspNetCore.Mvc;
using PlaywriteOT.Models;
using PlaywriteOT.Services;
using PlaywriteOT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT.Controllers
{
    public class NewsletterController : Controller
    {
        public IActionResult Subscription()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewsletterDashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendN()
        {
            NewsletterService ns = new NewsletterService();
            ns.createNewCampaign();
            return View();
        }

        [HttpGet]
        public ActionResult CreateSubscriber()
        {

            UserVM currentUser = AuthHold.Instance.currentUser;

            return View(currentUser.Email); //placeholder
        }

        /*[HttpPost]
        public ActionResult CreateSubscriber()
        {


            return View(); //placeholder
        }*/
    }
}
