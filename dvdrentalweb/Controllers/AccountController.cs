using dvdrentalweb.Areas.Identity.Pages.Account;
using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopContext _db;

        public AccountController(ShopContext db)
        {
            _db = db;
        }


    }
}
