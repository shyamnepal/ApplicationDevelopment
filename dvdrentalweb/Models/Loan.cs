using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class Loan
    {
        [Key]
        public int LoanNumber { get; set; }
        public int LoanNumberType { get; set; }
        public int CopyNumber { get; set; }
        public int MemberNumber { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateReturned { get; set; }

    }
}
