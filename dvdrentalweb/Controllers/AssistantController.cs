using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class AssistantController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
