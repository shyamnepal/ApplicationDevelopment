using dvdrentalweb.Models;
using Microsoft.AspNetCore.Mvc;

namespace dvdrentalweb.Controllers
{
    public class MemberController : Controller
    {
        private readonly ShopContext _db;

        public MemberController(ShopContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MemberList_03(string SearchText = "")
        {
            try
            {
                
                var memberList_01 = (from a in _db.Loans
                                     join b in _db.Members
                                     on a.MemberNumber equals b.MemberNumber
                                     join c in _db.DVDCopys
                                     on a.CopyNumber equals c.CopyNumber
                                     join d in _db.DVDTitles
                                     on c.DVDNumber equals d.DVDNumber
                                     where a.DateOut !>= DateTime.Now.AddDays(-31)


                                     select new Member
                                     {
                                         MemberNumber = a.MemberNumber,
                                         MembershipCategoryNumber = b.MembershipCategoryNumber,
                                         MemberLastNamae = b.MemberLastNamae,
                                         MemberFirstName = b.MemberFirstName,
                                         MemberAddress = b.MemberAddress,
                                         MemberDateOfBirth = b.MemberDateOfBirth,
                                         DateOut = a.DateOut,
                                         DVDTitle = d.DvdTitle,
                                         DateReturned = a.DateReturned
                                     }); 
        

        var memberListToList = memberList_01.ToList();
                if (SearchText != null && SearchText != "")
                {
                    memberListToList = memberList_01.
                        Where(b => b.MemberLastNamae.Contains(SearchText)
                        ).ToList();
                }
                return View(memberListToList);
            }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}
