using web.core.Data;

using System.ComponentModel.DataAnnotations;


namespace web.core.Models
{
    public class Rental:IEntity
    {
        public Guid Id{ get; set; }
        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Car car{ get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedAt{ get; set; }

        public Driver? Driver{get;set;}
        
        [Required]
        public int numOfDays{ get; set; }
     

    }
}
