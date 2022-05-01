using dvdrentalweb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ShopContext _db;

        public AdminController(ShopContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
