using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using metainf.Models;

namespace metainf.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Connections.Where(b => b.Id > 3).ToList();

            return View();

            //using (var context = serviceProvider.GetService<BloggingContext>())
            //{
            //    // do stuff
            //}
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
