using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT_v3.Controllers
{
    public class ServicesTipsController : Controller
    {
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
