using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ShopContext _db;

        public AdminController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
