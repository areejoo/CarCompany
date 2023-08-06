using System.ComponentModel.DataAnnotations;
using web.core.Entities;
namespace web.api.Dtos
{
    public class UpdateCarDto
    {
        [Required]
        public Guid Id { get; set; }

        public int Number { get; set; }

        public double EngineCapacity { get; set; }

        [StringLength(15)]
        public string Color { get; set; }

        [StringLength(10)]
        public string Type { get; set; }


        public double DailyFare { get; set; }


        public bool WithDriver { get; set; }

        public Guid? DriverId { get; set; }
    }
}