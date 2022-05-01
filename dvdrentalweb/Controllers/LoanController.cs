using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace dvdrentalweb.Controllers
{
    public class LoanController : Controller
    {
        private readonly ShopContext _db;

        public LoanController(ShopContext db)
        {
            _db = db;
        }


        public IActionResult LoanList_05(string SearchText = "")
        {
            try
            {
                var loanList_01 = (from a in _db.Loans
                                   join b in _db.DVDCopys
                                   on a.CopyNumber equals b.CopyNumber
                                   join c in _db.DVDTitles
                                   on b.DVDNumber equals c.DVDNumber
                                   join d in _db.Members
                                   on a.MemberNumber equals d.MemberNumber

                                   select new Loan
                                   {
                                       LoanNumber = a.LoanNumber,
                                       LoanTypeNumber = a.LoanTypeNumber,
                                       CopyNumber = a.CopyNumber,
                                       MemberNumber = a.MemberNumber,
                                       DateOut = a.DateOut,
                                       DateDue = a.DateDue,
                                       DateReturned = a.DateReturned,
                                       DVDTitle = c.DvdTitle,
                                       MemberLastName = d.MemberLastNamae,
                                       MemberFirstName = d.MemberFirstName,
                                   });

                var loanListToList = loanList_01.ToList();
                if (SearchText != null && SearchText != "")
                {
                    loanListToList = loanList_01.
                        Where(a => a.MemberNumber == int.Parse(SearchText)).ToList();
                }
                return View(loanListToList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        public IActionResult Index()
        {
            var loanList_01 = (from a in _db.Loans
                               join b in _db.LoanTypes
                               on a.LoanTypeNumber equals b.LoanTypeNumber
                               join c in _db.Members
                               on a.MemberNumber equals c.MemberNumber
                               join d in _db.DVDCopys
                               on a.CopyNumber equals d.CopyNumber
                               join e in _db.DVDTitles
                               on d.DVDNumber equals e.DVDNumber
                               join f in _db.DVDCategory
                               on e.CategoryNumber equals f.CategoryNumber
                               join g in _db.MembershipCategories
                               on c.MembershipCategoryNumber equals g.MembershipCategoryNumber

                               select new Loan
                               {
                                   LoanNumber = a.LoanNumber,
                                   LoanTypeNumber = a.LoanTypeNumber,
                                   CopyNumber = a.CopyNumber,
                                   MemberNumber = a.MemberNumber,
                                   MemberFirstName = c.MemberFirstName,
                                   MemberLastName = c.MemberLastNamae,
                                   DateOut = a.DateOut,
                                   DateDue = a.DateDue,
                                   DateReturned = a.DateReturned,
                                   LoanType = b.Loantype,
                                   DVDTitle = e.DvdTitle,
                                   StandardCharge = e.StandardCharge,
                                   PenaltyCharge = e.PenaltyCharge,
                                   AgeRestricted = f.AgeRestricted,
                                   MemberDateOfBirth = c.MemberDateOfBirth,
                                   MembershipCategoryTotalLoans = g.MembershipCategoryTotalLoans
                               }).ToList();
            return View(loanList_01);
        }

        //GET
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var loansFromDb = _db.Loans.Find(id);
            //var loansFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var loansFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (loansFromDb == null)
            {
                return NotFound();
            }

            return View(loansFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Loan obj)
        {
            ModelState.Remove("LoanType");
            ModelState.Remove("DVDTitle");
            ModelState.Remove("MemberAddress");
            ModelState.Remove("MemberLastName");
            ModelState.Remove("MemberFirstName");
            if (ModelState.IsValid)
            {
                _db.Loans.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult IssueIndex()
        {
            try
            {
                var availableDVD = (from a in _db.DVDTitles
                                    join b in _db.DVDCopys
                                    on a.DVDNumber equals b.DVDNumber

                                    select new Loan
                                    {
                                        DVDTitle = a.DvdTitle,
                                        CopyNumber = b.CopyNumber

                                    }).ToList();

                List<LoanType> loanTypeList = new List<LoanType>();
                loanTypeList = (from lt in _db.LoanTypes select lt).ToList();
                loanTypeList.Insert(0, new LoanType { LoanTypeNumber = 0, Loantype = "--Select Loan Type--" });
                ViewBag.loanType = loanTypeList;

                return View(availableDVD);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET
        public IActionResult Create()
        {
           return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan obj)
        {
            var loanListJoined =  from a in _db.Loans
                                  join b in _db.Members
                                  on a.MemberNumber equals b.MemberNumber
                                  join c in _db.DVDCopys
                                  on a.CopyNumber equals c.CopyNumber
                                  join d in _db.DVDTitles
                                  on c.DVDNumber equals d.DVDNumber
                                  join e in _db.DVDCategory
                                  on d.CategoryNumber equals e.CategoryNumber
                                  join f in _db.LoanTypes
                                  on a.LoanTypeNumber equals f.LoanTypeNumber

                                  select new Loan
                                  {
                                      LoanNumber = a.LoanNumber,
                                      LoanTypeNumber = a.LoanTypeNumber,
                                      LoanDuration = f.LoanDuration,
                                      CopyNumber = a.CopyNumber,
                                      MemberNumber = a.MemberNumber,
                                      MemberDateOfBirth = b.MemberDateOfBirth,
                                      DateOut = a.DateOut,
                                      DateDue = a.DateDue,
                                      DateReturned = a.DateReturned,
                                  };

            var today = DateTime.Now.Year;
            var age = today - obj.MemberDateOfBirth.Year;
            if(age >= obj.AgeRestricted)
            {
                _db.Loans.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj); 
        }

        public IActionResult LoanList_07()
        {
            try
            {
                var loanList_01 = (from a in _db.Loans
                                   join b in _db.Members
                                   on a.MemberNumber equals b.MemberNumber
                                   join c in _db.DVDCopys
                                   on a.CopyNumber equals c.CopyNumber
                                   join d in _db.DVDTitles
                                   on c.DVDNumber equals d.DVDNumber                                  

                                   select new Loan
                                   {
                                       LoanNumber = a.LoanNumber,
                                       LoanTypeNumber = a.LoanTypeNumber,
                                       CopyNumber = a.CopyNumber,
                                       MemberNumber = a.MemberNumber,
                                       DateOut = a.DateOut,
                                       DateDue = a.DateDue,
                                       DateReturned = a.DateReturned,
                                       DVDTitle = d.DvdTitle,
                                       PenaltyCharge = d.PenaltyCharge                                       
                                   }).ToList();        
                
                return View(loanList_01);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult LoanList_11()
        {
            try
            {
                var loanList_11 = (from a in _db.Loans
                                   join b in _db.DVDCopys
                                   on a.CopyNumber equals b.CopyNumber
                                   join c in _db.DVDTitles
                                   on b.DVDNumber equals c.DVDNumber
                                   join d in _db.Members
                                   on a.MemberNumber equals d.MemberNumber
                                   join e in _db.MembershipCategories
                                   on d.MembershipCategoryNumber equals e.MembershipCategoryNumber                                  

                                   select new Loan
                                   {
                                       DVDTitle = c.DvdTitle,
                                       CopyNumber = b.CopyNumber,
                                       MemberFirstName = d.MemberFirstName,
                                       MemberLastName = d.MemberLastNamae,
                                       DateOut = a.DateOut,
                                       MembershipCategoryTotalLoans = e.MembershipCategoryTotalLoans
                                   });
                
                var loanListToList = loanList_11.ToList();

                return View(loanListToList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult LoanList_12()
        {
            try
            {
                var loanList_13 = (from a in _db.Loans
                                   join b in _db.DVDCopys
                                   on a.CopyNumber equals b.CopyNumber
                                   join c in _db.DVDTitles
                                   on b.DVDNumber equals c.DVDNumber
                                   join d in _db.Members
                                   on a.MemberNumber equals d.MemberNumber
                                   where a.DateOut <= DateTime.Now.AddDays(-31)

                                   select new Loan
                                   {
                                       MemberFirstName = d.MemberFirstName,
                                       MemberLastName = d.MemberLastNamae,
                                       MemberAddress = d.MemberAddress,
                                       DateOut = a.DateOut,
                                       DVDTitle = c.DvdTitle,
                                   });

                var loanListToList = loanList_13.ToList();

                return View(loanListToList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult LoanList_13()
        {
            try
            {
                var loanList_13 = (from a in _db.Loans
                                   join b in _db.DVDCopys
                                   on a.CopyNumber equals b.CopyNumber
                                   join c in _db.DVDTitles
                                   on b.DVDNumber equals c.DVDNumber
                                   where a.DateOut <= DateTime.Now.AddDays(-30)

                                   select new Loan
                                   {                                       
                                       DateOut = a.DateOut,
                                       DVDTitle = c.DvdTitle,
                                   });

                var loanListToList = loanList_13.ToList();
          
                return View(loanListToList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
