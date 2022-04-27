using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class DVDCopyController : Controller
    {
        private readonly ShopContext _db;

        public DVDCopyController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult DVDCopyList_10()
        {
            try
            {

                var DvDCopyList_10 = (from a in _db.DVDCopys
                                      join b in _db.DVDTitles
                                      on a.DVDNumber equals b.DVDNumber
                                      join c in _db.Loans
                                      on a.CopyNumber equals c.CopyNumber
                                      where b.DateReleased <= DateTime.Now.AddDays(-365)
                                      where c.DateReturned != null                    

                                       select new DVDCopy
                                       {
                                           DVDNumber = a.DVDNumber,
                                           CopyNumber = a.CopyNumber,
                                           DatePurchase = a.DatePurchase,
                                           DVDTitle = b.DvdTitle,
                                           DateReleased = b.DateReleased,
                                           DateReturned = c.DateReturned
                                       });


                var DvDCopyList = DvDCopyList_10.ToList();
                return View(DvDCopyList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET
        public IActionResult Delete(int? CopyNumber)
        {
            if (CopyNumber == null || CopyNumber == 0)
            {
                return NotFound();
            }
            var DVDCopyFromDb = _db.DVDCopys.Find(CopyNumber);
            if (DVDCopyFromDb == null)
            {
                return NotFound();
            }
            return View(DVDCopyFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult DeletePOST(int? CopyNumber)
        {
            var DVDCopyFromDb = _db.DVDCopys.Find(CopyNumber);
            if (DVDCopyFromDb == null)
            {
                return NotFound();
            }
            
            _db.DVDCopys.Remove(DVDCopyFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
            
        }


    }
}
