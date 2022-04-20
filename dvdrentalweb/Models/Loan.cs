using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class Loan
    {
        [Key]
        public int LoanNumber { get; set; }
        [ForeignKey("LoanTyoeNumber")]
        public int LoanTypeNumber { get; set; }
        [ForeignKey("CopyNumber")]
        public int CopyNumber { get; set; }
        [ForeignKey("MemberNumber")]
        public int MemberNumber { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime DateReturned { get; set; }
        [NotMapped]
        public string DVDTitle { get; set; }
        [NotMapped]
        public string MemberLastName { get; set; }
        [NotMapped]
        public string MemberFirstName { get; set; }

    }
}
