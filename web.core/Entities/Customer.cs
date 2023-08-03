
using System.ComponentModel.DataAnnotations;


namespace web.core.Entities
{
    public class Customer : BaseEntity
    {

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

    }
}
