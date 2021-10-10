using Microsoft.AspNetCore.Mvc;
using PlaywriteOT_v3.Models;
using PlaywriteOT_v3.Services;
using PlaywriteOT_v3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Controllers
{
    public class NewsletterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NewUpload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewUpload(string headingT, string bodyT, string url)
        {
            NewsletterService ns = new NewsletterService();
            //call cloudinary Upload 
            //pass through newsletter or get link through upload component

            try
            {
                if (!ns.CreateNewCampaign(headingT, bodyT, url))
                {
                    ViewBag.Error = "Makes sure all fields are filled in and you have uploaded a newsletter";
                    return View();
                }
                return RedirectToAction("SentStatus");
            }
            catch (Exception)
            {
                ViewBag.Error = "Newsletters failed to send";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Subscription()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unsubscribe()  //SendInBlue has an integrated page
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateSubscriber()
        {
            UserVM currentUser = AuthHold.Instance.currentUser;

            return View(currentUser.Email); //placeholder
        }

        [HttpGet]
        public IActionResult SentStatus()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult CreateSubscriber()
        {
            return View(); //placeholder
        }*/
    }
}

