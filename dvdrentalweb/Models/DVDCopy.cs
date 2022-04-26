using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class DVDCopy
    {
        [Key]
        public int CopyNumber { get; set; }
        [ForeignKey("DVDNumber")]
        public int DVDNumber { get; set; }
        public DateTime DatePurchase{ get; set; }
        [NotMapped]
        public string DVDTitle{ get; set; }
        [NotMapped]
        public DateTime? DateReturned { get; set; }
        [NotMapped]
        public DateTime? DateReleased { get; set; }

    }
}
