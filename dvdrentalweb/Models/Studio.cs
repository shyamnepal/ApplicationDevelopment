using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class Studio
    {
        [Key]
        public int StudioNumber { get; set; }
        public String StudioName { get; set; }
       
    }
}
