using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class MembershipCategory
    {
        [Key]
        public int MembershipCategoryNumber { get; set; }
        public String MembershipCategoryDescription { get; set; }
        public int MembershipCategoryTotalLoans { get; set; }
        
    }
}
