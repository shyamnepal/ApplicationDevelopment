using System.ComponentModel.DataAnnotations;

namespace dvdrentalweb.Models
{
    public class Producer
    {
        [Key]
        public int ProducerNumber { get; set; }
        public String ProducerName { get; set; }
        
    }
}
