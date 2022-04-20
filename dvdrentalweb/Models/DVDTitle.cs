using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class DVDTitle
    {
        [Key]
        public int DVDNumber { get; set; }
        [ForeignKey("CategoryNumber")]
        public int CategoryNumber { get; set; }
        [ForeignKey("StudioNumber")]
        public int StudioNumber { get; set; }
        [ForeignKey("ProducerNumber")]
        public int ProducerNumber { get; set; }
        public String DvdTitle { get; set; }
        public DateTime DateReleased { get; set; }
        public double StandardCharge { get; set; }
        public double PenaltyCharge { get; set; }

        [NotMapped]
        public string ProducerName { get; set; }
        [NotMapped]
        public string StudioName { get; set; }
        [NotMapped]
        public string ActorSurname { get; set; }
        [NotMapped]
        public string ActorFirstName { get; set; }

    }
}
