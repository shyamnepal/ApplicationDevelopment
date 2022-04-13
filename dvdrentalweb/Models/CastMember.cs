using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class CastMember
    {
        [Key]
        public int DVDNumber { get; set; }
        public int ActorNumber { get; set; }
      
    }
}
