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
                        Where(a => a.CopyNumber == int.Parse(SearchText)).ToList();
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
            obj.DateReturned = DateTime.Now;
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

        //GET
        public IActionResult Create()
        {
            var copyInStock =  from a in _db.DVDTitles
                               join b in _db.DVDCopys
                               on a.DVDNumber equals b.DVDNumber
                               join e in _db.Loans
                               on b.CopyNumber equals e.CopyNumber into f
                               from e in f.DefaultIfEmpty()
                               where e.DateReturned != null
                               orderby b.CopyNumber
                               select new Loan
                               {
                                   DVDTitle = a.DvdTitle,
                                   CopyNumber = b.CopyNumber
                               };

            ViewBag.loanType = new SelectList(_db.LoanTypes, "LoanTypeNumber", "Loantype");
            ViewBag.dvdTitles = new SelectList(copyInStock, "CopyNumber", "DVDTitle");
            ViewBag.memberName = new SelectList(_db.Members, "MemberNumber", "MemberFirstName");
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan obj)
        {      

            // Check for Age Restriction:
            int memberNumber = obj.MemberNumber;
            int copyNumberForAgeRestricted = obj.CopyNumber;

            // Check for number of loans
            var memberLoanCount = _db.Loans.GroupBy(a => a.MemberNumber).Select(g => new { Key = g.Key, Count = g.Count() });
            
            try
            {
                obj.DateOut = DateTime.Now;
                int daysDue = GetLoanDuration(obj.LoanTypeNumber);
                obj.DateDue = DateTime.Now.AddDays(daysDue);
                int memberAge = GetAge(obj.MemberNumber);
                int ageRestriction = GetAgeRestricted(obj.CopyNumber);
                int totalLoanCount = GetMembersCurrentNumberOfLoans(obj.MemberNumber);
                int allowedLoanCount = GetMemberTotalAllowedLoans(obj.MemberNumber);
                if (memberAge > ageRestriction)
                {
                    if(totalLoanCount <= allowedLoanCount)
                    {
                        ModelState.Remove("DVDTitle");
                        ModelState.Remove("LoanType");
                        ModelState.Remove("MemberAddress");
                        ModelState.Remove("MemberFirstName");
                        ModelState.Remove("MemberLastName");
                        if (ModelState.IsValid)
                        {
                            _db.Loans.Add(obj);
                            _db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch
            {
                return View(obj);
            }
            return View(obj); 
            
            int GetAgeRestricted(int copyNumber)
            {
                int DvdNumber = _db.DVDCopys.Find(copyNumber).DVDNumber;
                int DvdCategoryNumber = _db.DVDTitles.Find(DvdNumber).CategoryNumber;
                var DvdCategory = _db.DVDCategory.Find(DvdCategoryNumber);
                int ageRestricted = DvdCategory.AgeRestricted;
                return ageRestricted;
            }

            int GetAge(int memberNumber)
            {
                var Member = _db.Members.Find(memberNumber);
                var DOB = Member.MemberDateOfBirth;
                int age = DateTime.Now.Year - DOB.Year;
                return age;
            }

            int GetLoanDuration(int? loanTypeNumber)
            {
                int loanDuration = _db.LoanTypes.Find(loanTypeNumber).LoanDuration;                
                return loanDuration;
            }

            int GetMemberTotalAllowedLoans(int memberNumber)
            {
                int MemberCategoryNumber = _db.Members.Find(memberNumber).MembershipCategoryNumber;
                int MembershipCategoryTotalLoans = _db.MembershipCategories.Find(MemberCategoryNumber).MembershipCategoryTotalLoans;
                return MembershipCategoryTotalLoans;
            }

            int GetMembersCurrentNumberOfLoans(int memberNumber)
            {
                var totalLoansNotReturned = (from a in _db.Loans
                                            where a.DateReturned == null
                                            select a).ToList();
                var groups = totalLoansNotReturned.GroupBy(n => n.MemberNumber)
                         .Select(n => new
                         {
                             number = n.Key,
                             loanCount = n.Count()
                         }).ToList();
                var member = groups.Where(x => x.number == memberNumber);
                int loanCount = member.Count();
                return loanCount;
            }
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
                                   where a.DateReturned == null
                                   orderby a.DateOut

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
                                   into f
                                   from b in f.DefaultIfEmpty()
                                   join c in _db.DVDTitles
                                   on b.DVDNumber equals c.DVDNumber
                                   where a.DateOut <= DateTime.Now.AddDays(-31)

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
