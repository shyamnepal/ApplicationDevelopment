using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dvdrentalweb.Controllers
{
    public class ActorController : Controller
    {
        private readonly ShopContext _db;

        public ActorController(ShopContext db)
        {
            _db = db;
        }
        public IActionResult ActorList_01(string SearchText="")
        {
            try
            {
                var actorList_01 = (from  a in _db.Actors
                                    join b in _db.CastMembers
                                    on   a.ActorNumber equals b.ActorNumber
                                    join c in _db.DVDTitles
                                    on b.DVDNumber equals c.DVDNumber
                                    join d in _db.DVDCopys
                                    on c.DVDNumber equals d.DVDNumber
                                    orderby d.CopyNumber

                                    select new Actor
                                    {
                                        ActorNumber = a.ActorNumber,
                                        ActorSurName = a.ActorSurName,
                                        ActorFirstName = a.ActorFirstName,
                                        DVDTitle = c.DvdTitle,
                                        CopyNumber = d.CopyNumber
                                    });

                var actorListToList = actorList_01.ToList();
                if (SearchText != null && SearchText != "")
                {
                    actorListToList = actorList_01.
                        Where(a => a.ActorSurName.Contains(SearchText)).ToList();
                }
                return View(actorListToList);
            }
            catch (Exception ex) {
                return View();
            }

        }

        public IActionResult Index()
        {
            var actorList = _db.Actors.ToList();
            return View(actorList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Actor obj)
        {
            ModelState.Remove("DVDTitle");
            if (ModelState.IsValid)
            {
                _db.Actors.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
