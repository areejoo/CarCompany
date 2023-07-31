using System.ComponentModel.DataAnnotations;
using web.core.Data;

namespace web.core.Models
{
    public class Car:IEntity
    {
        public Guid Id { get; set; }
        // [Index(nameof(Number), IsUnique = true)]
        public int Number{ get; set; }

        [Required]
        public float EngineCapacity{ get; set; }
        
        [Required]
        [StringLength(20)]
        public string Color{ get; set; }

        [Required]
        [StringLength(20)]
        public string Type{ get; set; }

        [Required]
        [StringLength(20)]
        public float DailyFare{ get; set; }
        
        [Required]
        public bool WithDriver{ get; set; }

        [Required]
        public Driver Driver{get; set;}





    }

}
