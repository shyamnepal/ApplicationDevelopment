using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; }
        public String DVDNumber { get; set; }
        public DateTime DatePurchase{ get; set; }
        
    }
}
