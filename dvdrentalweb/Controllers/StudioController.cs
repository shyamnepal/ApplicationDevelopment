using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class StudioController : Controller
    {
        private readonly ShopContext _db;

        public StudioController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objStudioList = _db.Studios.ToList();
            return View(objStudioList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Studio obj)
        {
            if (ModelState.IsValid)
            {
                _db.Studios.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
