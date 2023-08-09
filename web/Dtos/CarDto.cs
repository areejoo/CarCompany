using System.ComponentModel.DataAnnotations;

namespace web.api.Dtos
{
    public class CarDto
    {
        [Required]
        public int Number { get; set; }

        public double EngineCapacity { get; set; }

        public string Color { get; set; }


        public string Type { get; set; }


        public double DailyFare { get; set; }


        public bool WithDriver { get; set; }

        public Guid? DriverId { get; set; }
    }
}