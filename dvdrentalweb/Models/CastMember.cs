using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class CastMember
    {
        [Key]
        public int CastMemberId { get; set; }
        [ForeignKey("DVDNumber")]
        public int DVDNumber { get; set; }
        [ForeignKey("ActorNumber")]
        public int ActorNumber { get; set; }
      
    }
}
