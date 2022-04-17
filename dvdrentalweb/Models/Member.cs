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
        
    }
}
