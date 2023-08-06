using System.ComponentModel.DataAnnotations;

namespace web.api.Dtos
{
    public class CarListDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Number { get; set; }


        public double EngineCapacity { get; set; }

        public string Color { get; set; }


        public string Type { get; set; }


        public double DailyFare { get; set; }


        public bool WithDriver { get; set; }

        public Guid? DriverId { get; set; }
        
        [Required]
        public int Count { get; set; }
    }
}