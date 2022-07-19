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

                TempData["suc"] = "Created SUCESSFULLY!!";
                return RedirectToAction("Index");

            }

            return View("Create");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();  
            }

            var res = _db.mytables.Find(id);

            if(res == null)
            {
                return NotFound();
            }

            return View(res);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(mytable obj)
        {

            if (ModelState.IsValid)
            {
                _db.mytables.Update(obj);
                _db.SaveChanges();
                TempData["suc"] = "Edit SUCESSFULLY!!";
                return RedirectToAction("Index");

            }

            return View("Create");
        }


        public IActionResult Del(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            else
            {
                var res = _db.mytables.Find(id);

                if (res == null)
                {
                    return NotFound();
                }
                _db.mytables.Remove(res);
                _db.SaveChanges();
                TempData["suc"] = "Deleted SUCESSFULLY!!";
            }

            return RedirectToAction("Index");
        }
    }
}
