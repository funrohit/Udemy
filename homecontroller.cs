using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Udemy.db;
using Udemy.Models;

namespace Udemy.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbdata _db;

        public HomeController(dbdata db)
        {
            _db = db;   
        }

        public IActionResult Index()
        {

            IEnumerable<mytable> mm = _db.mytables;    

            return View(mm);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(mytable obj)
        {

            if (ModelState.IsValid)
            {
                _db.mytables.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View("Create");
        }
    }
}
