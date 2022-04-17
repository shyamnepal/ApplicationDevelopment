using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public String DVDNumber { get; set; }
        public DateTime DatePurchase{ get; set; }
        
    }
}
