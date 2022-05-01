using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class Actor
    {
        [Key]
        public int ActorNumber { get; set; }
        public String ActorSurName { get; set; }
        public String ActorFirstName { get; set; }

        [NotMapped]
        public string DVDTitle { get; set; }
        [NotMapped]
        public int CopyNumber { get; set; }
        [NotMapped]
        public DateTime DateReturned { get; set; }
    }
}
