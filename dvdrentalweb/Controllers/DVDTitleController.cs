using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dvdrentalweb.Controllers
{
    public class DVDTitleController : Controller
    {
        private readonly ShopContext _db;

        public DVDTitleController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objDVDTitleList = _db.DVDTitles.ToList();
            return View(objDVDTitleList);
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.producerName = new SelectList(_db.Producers, "ProducerNumber", "ProducerName");
            ViewBag.categoryName = new SelectList(_db.DVDCategory, "CategoryNumber", "CategoryDescription");
            ViewBag.studioName = new SelectList(_db.Studios, "StudioNumber", "StudioName");
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(DVDTitle obj)
        {
            ModelState.Remove("StudioName");
            ModelState.Remove("ActorFirstName");
            ModelState.Remove("ActorSurName");
            ModelState.Remove("ProducerName");
            if (ModelState.IsValid)
            {
                _db.DVDTitles.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult DVDTitleList_02(string SearchText = "")
        {
            var dvdTitleList = (from a in _db.DVDTitles
                               join b in _db.DVDCopys
                               on a.DVDNumber equals b.DVDNumber
                               join c in _db.CastMembers
                               on b.DVDNumber equals c.DVDNumber
                               join d in _db.Actors
                               on c.ActorNumber equals d.ActorNumber
                               join e in _db.Loans
                               on b.CopyNumber equals e.CopyNumber into f
                               from e in f.DefaultIfEmpty()
                               where e.DateReturned != null
                               orderby b.CopyNumber
                               select new DVDTitle
                               {
                                   DvdTitle = a.DvdTitle,
                                   CopyNumber = b.CopyNumber,
                                   ActorFirstName = d.ActorFirstName,
                                   ActorSurname = d.ActorSurName
                               }).ToList();
            var dvdTitleToList = dvdTitleList.ToList();
            if (SearchText != null && SearchText != "")
            {
                dvdTitleToList = dvdTitleList.
                    Where(a => a.ActorSurname.Contains(SearchText)).ToList();
            }
            return View(dvdTitleToList);
            
        }


        public IActionResult DVDTitleList_04()
        {
            try
            {

                var DvDTitleList_01 = (from a in _db.DVDTitles
                                     join b in _db.Producers
                                     on a.ProducerNumber equals b.ProducerNumber
                                     join c in _db.Studios
                                     on a.StudioNumber equals c.StudioNumber
                                     join d in _db.CastMembers
                                     on a.DVDNumber equals d.DVDNumber
                                     join e in _db.Actors
                                     on d.ActorNumber equals e.ActorNumber
                                     orderby a.DateReleased, e.ActorFirstName

                                     select new DVDTitle
                                     {
                                         DVDNumber = a.DVDNumber,
                                         CategoryNumber = a.CategoryNumber,
                                         StudioNumber = a.StudioNumber,
                                         ProducerNumber = a.ProducerNumber,
                                         DvdTitle = a.DvdTitle,
                                         DateReleased = a.DateReleased,
                                         StandardCharge = a.StandardCharge,
                                         PenaltyCharge = a.PenaltyCharge,
                                         ProducerName = b.ProducerName,
                                         StudioName = c.StudioName,
                                         ActorSurname = e.ActorSurName,
                                         ActorFirstName = e.ActorFirstName
                                     });


                var DvDTitleList = DvDTitleList_01.ToList();
                return View(DvDTitleList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
