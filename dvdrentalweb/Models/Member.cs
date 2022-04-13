using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        public int MembershipCategoryNumber { get; set; }
        public String MemberLastNamae { get; set; }
        public String MemberFirstName { get; set; }
        public String MemberAddress { get; set; }
        public DateTime MemberDateOfBirth { get; set; }
        
    }
}
