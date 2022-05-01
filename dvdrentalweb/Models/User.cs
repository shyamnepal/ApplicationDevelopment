using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class User
    {
        [Key] 
        public int ID { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public String UserType { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public String UserPassword { get; set; }
        
    }
}
