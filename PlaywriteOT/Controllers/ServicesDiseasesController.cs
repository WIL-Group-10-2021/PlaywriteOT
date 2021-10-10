using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Controllers
{
    public class ServicesDiseasesController : Controller
    {
        public IActionResult AutismSpectrumDisorder()
        {
            return View();
        }
        public IActionResult SensoryProcessingDisorder()
        {
            return View();
        }
        public IActionResult CerebralPalsy()
        {
            return View();
        }
        public IActionResult DownSyndrome()
        {
            return View();
        }
        public IActionResult DevelopmentalDelay()
        {
            return View();
        }
        public IActionResult LearningDifficulties()
        {
            return View();
        }
    }
}
