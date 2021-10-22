using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using PlaywriteOT.Interfaces;
using PlaywriteOT.Models;
using PlaywriteOT.Services;
using PlaywriteOT.Utilities;

namespace PlaywriteOT.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private string _generatedToken = null;

        public UserController(IConfiguration config, ITokenService tokenService)
        {
            _config = config;
            _tokenService = tokenService;  //injection
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))    //check for null values
            {
                ViewBag.Error = "Please enter details";
                return View();
            }

            if (await AuthHold.Instance.LoginUser(email, password))  //login user
            {
                _generatedToken = _tokenService.BuildToken(_config["Jwt:Key"], _config["Jwt:Issuer"].ToString(), AuthHold.Instance.currentUser);
                
                if (_generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", _generatedToken);                                                    //store token
                    LoggingService.WriteLog(new Log(this.GetType().FullName, "Login success: " + email));         //new log for login success
                    return RedirectToAction("AdminHome", "Home");                                                   //successfully logged in
                }
            }
            LoggingService.WriteLog(new Log(this.GetType().FullName, "Login failed: " + email));                  //new log for login fail
            ViewBag.Error = "Incorect username and password combination";
            return View();

        }

        [HttpGet]
        public IActionResult Register()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }                                        //validates token
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
                    LoggingService.WriteLog(new Log(this.GetType().FullName, "Registration: " + email)); 
                    return RedirectToAction("Login");
                }
                else
                {
                    LoggingService.WriteLog(new Log(this.GetType().FullName, "Register - Email already exists: " + email)); 
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
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
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