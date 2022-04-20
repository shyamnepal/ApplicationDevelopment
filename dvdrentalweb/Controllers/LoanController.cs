using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

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
                //if (SearchText != null && SearchText != "")
                //{
                //    loanListToList = loanList_01.
                //        Where(a => a.MemberNumber == int.Parse(SearchText)).ToList();
                //}
                return View(loanListToList);
            }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}
