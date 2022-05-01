using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ShopContext _db;

        public ProducerController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objProducerList = _db.Producers.ToList();
            return View(objProducerList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Producer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Producers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
