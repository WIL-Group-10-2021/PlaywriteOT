using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PlaywriteOT.Interfaces;
using PlaywriteOT.Models;
using PlaywriteOT.Services;
using PlaywriteOT.Utilities;

namespace PlaywriteOT.Controllers
{
    public class NewsletterController : Controller
    {

        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public NewsletterController(IConfiguration config, ITokenService tokenService)
        {
            _config = config;
            _tokenService = tokenService;  //injection
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
            NewsletterService ns = new NewsletterService();
            ViewBag.NumEmails = ns.DashboardInfo().Count;       //get summary of newsletters sent
            
            return View();
        }

        [HttpGet]
        public IActionResult NewUpload()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
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
                LoggingService.WriteLog(new Log(this.GetType().FullName, "Campaign sent by" + AuthHold.Instance.currentUser.Email));
                return RedirectToAction($"SentStatus");
            }
            catch (Exception)
            {
                LoggingService.WriteLog(new Log(this.GetType().FullName, "Campaign failed send by:" + AuthHold.Instance.currentUser.Email));
                ViewBag.Error = "Newsletters failed to send";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Subscription()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
            return View();
        }

        [HttpGet]
        public IActionResult Unsubscribe()  //SendInBlue has an integrated page
        {
            return View();
        }


/*      [HttpGet]
        public ActionResult CreateSubscriber()
        {
            UserVM currentUser = AuthHold.Instance.currentUser;

            return View(currentUser.Email); //placeholder
        }*/
        
        [HttpGet]
        public IActionResult SentStatus()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
            return View();
        }

        /*[HttpPost]
        public ActionResult CreateSubscriber()
        {
            return View(); //placeholder
        }*/


        /// <summary>
        /// Checks that that user is logged in and has a valid token
        /// </summary>
        /// <returns>True if valid token</returns>
        private bool IsLoggedIn()
        {
            string token = HttpContext.Session.GetString("Token");                                                           // gets JWT from session 
            if (token == null) { return false; }                                                                                 // no token => back to login
            if (_tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return true;        //valid token
            }
            return false;
            //ViewBag.Message = BuildMessage(token, 50);
        }
    }
}

