using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlaywriteOT_v3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /*
         * MAIN PAGES - header
         */

        // home
        public IActionResult Index()
        {
            return View();
        }

        // about
        public IActionResult About()
        {
            return View();
        }

        /*
         * SUBPAGES UNDER MAIN PAGES are formatted as follow:
         * 
         * MAIN_SUBPAGE_CONTROLLER
         * 
         * example: ServicesDiseaseController
         */

        // services
        public IActionResult Services()
        {
            return View();
        }

        // contact
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
    }
}
