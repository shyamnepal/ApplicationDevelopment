using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        [ForeignKey("MembershipCategoryNumber")]
        public int MembershipCategoryNumber { get; set; }
        public String MemberLastNamae { get; set; }
        public String MemberFirstName { get; set; }
        public String MemberAddress { get; set; }
        public DateTime MemberDateOfBirth { get; set; }

        [NotMapped]
        public DateTime DateOut { get; set; }
        [NotMapped]
        public DateTime DateReturned { get; set; }
        [NotMapped]
        public string DVDTitle { get; set; }    
        [NotMapped]
        public DateTime DateDue { get; set; }
        [NotMapped]
        public int TotalLoans { get; set; }
        [NotMapped]
        public int TotalAllowedLoans { get; set; }
    }
}
