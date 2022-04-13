using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        public int CategoryNumber { get; set; }
        public int StudioNumber { get; set; }
        public int ProducerNumber { get; set; }
        public String DvdTitle { get; set; }
        public DateTime DateReleased { get; set; }
        public double StandardCharge { get; set; }
        public double PenaltyCharge { get; set; }
    }
}
