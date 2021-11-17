using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
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
        public IActionResult TipsForTherapists()
        {
            return View();
        }
        public IActionResult TipsForTeachers()
        {
            return View();
        }
        public IActionResult TipsForParents()
        {
            return View();
        }

    }
}
