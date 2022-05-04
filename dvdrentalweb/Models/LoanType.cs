using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dvdrentalweb.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public string Loantype { get; set; }
        public int LoanDuration { get; set; }
        
    }
}
