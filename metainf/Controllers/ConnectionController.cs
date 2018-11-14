using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using metainf.Models;

namespace metainf.Controllers
{
    public class ConnectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Connection connection)
        {
            //return Content($"Hello a");
            return View("New");
        }
    }
}
