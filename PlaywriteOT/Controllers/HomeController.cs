using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PlaywriteOT.Interfaces;
using PlaywriteOT.Models;

namespace PlaywriteOT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }

        
        public IActionResult AdminHome()
        {
            if (!IsLoggedIn()) { return RedirectToAction("Login", "User"); }//validates token
            return View();
        }
        public IActionResult Contact()
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
        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize).Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));
            string result = "The generated token is:";
            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }
            return result;
        }
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
