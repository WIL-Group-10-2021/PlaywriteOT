using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaywriteOT_v3.Models;
using PlaywriteOT_v3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // LOGIN
        [HttpGet]
        public IActionResult Login()
        {
            /*NewsletterService ns = new NewsletterService();
            if (ns.CreateNewCampaign("","",""))
            {
                return View();
            }*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (await AuthHold.Instance.LoginUser(email, password))
            {
                HttpContext.Session.SetString("LoggedInUser", email);
                return RedirectToAction("AdminHome", "Home");
            }

            ViewBag.Error = "Incorect username and password combination";
            return View();

        }

        // REGISTER
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            //user entered the matching passwords
            if (password.Equals(confirmPassword)) 
            {
                UserVM userVM = new UserVM()
                {
                    FName = firstName,
                    LName = lastName,
                    Email = email
                };

                //if registration successsfull
                if (await AuthHold.Instance.RegisterUser(userVM, password))
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Email already exists";
                    return View();
                }
            }
            else //passwords do not match
            {
                ViewBag.Error = "Passwords do not match";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Profile(string fname, string lname, string emailAddress, string psw, string pswConfirm)
        {

            if (psw.Equals(pswConfirm)) //user entered the matching passwords
            {
                //then do da thang

                UserVM userVM = new UserVM();
                userVM.FName = fname;
                userVM.LName = lname;
                userVM.Email = emailAddress;
                
                await AuthHold.Instance.UpdateUserDetails(userVM);

            }
            else //passwords do not match
            {
                //try again noob
            }

            return View();
        }*/
    }
}