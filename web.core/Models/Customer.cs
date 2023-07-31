using web.core.Data;

using System.ComponentModel.DataAnnotations;


namespace web.core.Models
{
    public class Customer:IEntity
    {
        public Guid Id{ get; set; }
        public String Name { get; set; }
        [Required]
        [StringLength(10)]
        public String Phone{ get; set; }

        [Required]
        [StringLength(30)]
        public String Email{ get; set; }
        
        [Required]
        [StringLength(50)]
        public String Address{ get; set; }
       
        public ICollection<Car> Cars{ set; get; }

    }
}
