using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class LoanType
    {
        [Key]
        public int LoanTypeNumber { get; set; }
        public String Loantype { get; set; }
        public int LoanDuration { get; set; }
        
    }
}
