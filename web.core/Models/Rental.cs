using web.core.Data;

using System.ComponentModel.DataAnnotations;


namespace web.core.Models
{
    public enum StatusRental{
        Canceled,
        Rented,
        Returned,
        // Pending


    }
    public class Rental:IEntity
    {
        public Guid Id{ get; set; }

        public Customer Customer { get; set; }

        public Car Car{ get; set; }

        public StatusRental Status{ get; set; }
        
        public DateTime CreatedAt{ get; set; }

        public Driver? Driver{get;set;}
    
        public int RentTerm{ get; set; }
    
    }
}
