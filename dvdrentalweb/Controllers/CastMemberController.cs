using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dvdrentalweb.Controllers
{
    public class CastMemberController : Controller
    {
        private readonly ShopContext _db;

        public CastMemberController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objCastMemberList = _db.CastMembers.ToList();
            return View(objCastMemberList);
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.dvdTitleName = new SelectList(_db.DVDTitles, "DVDNumber", "DvdTitle");
            ViewBag.actorName = new SelectList(_db.Actors, "ActorNumber", "ActorSurName");
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(CastMember obj)
        {
            if (ModelState.IsValid)
            {
                _db.CastMembers.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
