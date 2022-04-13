using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class User
    {
        [Key]
        public int UserNumber { get; set; }
        public String UserName { get; set; }
        public String UserType { get; set; }
        public String UserPassword { get; set; }
        
    }
}
