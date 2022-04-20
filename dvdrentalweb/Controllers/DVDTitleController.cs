using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

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
            return View();
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
