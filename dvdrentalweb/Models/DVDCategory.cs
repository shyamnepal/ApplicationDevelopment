using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class DVDCategory
    {
        [Key]
        public int CategoryNumber { get; set; }
        public String CategoryDescription { get; set; }
        public int AgeRestricted { get; set; }
        
    }
}
