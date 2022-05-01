using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class ApplicationUser: IdentityUser 
    {
        [Required]
        public string UserName{ get; set; }
        
        public string? UserType { get; set; }
    }
}
