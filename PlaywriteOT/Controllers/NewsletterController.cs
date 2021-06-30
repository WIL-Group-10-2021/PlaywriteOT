using Microsoft.AspNetCore.Mvc;
using PlaywriteOT.Models;
using PlaywriteOT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT.Controllers
{
    public class NewsletterController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/

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
