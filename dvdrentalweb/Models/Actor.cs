using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class Actor
    {
        [Key]
        public int ActorNumber { get; set; }
        public String ActorSurName { get; set; }
        public String ActorFirstName { get; set; }
    }
}
