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
        private readonly MainContext _context;

        public ConnectionController(MainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Connection.ToList());
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            return View(_context.Connection.Where(x => x.Id.Equals(id)).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Save(Connection connection)
        {
            if(connection.Id == 0)
                _context.Add(connection);
            else
                _context.Update(connection);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _context.Remove(new Connection { Id = id });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
