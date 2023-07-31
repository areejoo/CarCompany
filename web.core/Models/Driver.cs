using web.core.Data;

using System.ComponentModel.DataAnnotations;

namespace web.core.Models
{
    public class Driver:IEntity
    {
        public Guid Id{ get; set; }
        
        [Required]
        [StringLength(30)]
        public String Name { get; set; }
       
        [Required]
        [StringLength(10)]
        public String Phone{ get; set; }

        [Required]
        public bool IsAvailable{get;set;}

        public Car Car{ get; set; }





    }
}
