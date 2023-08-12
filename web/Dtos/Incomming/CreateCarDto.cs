using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using web.core.Entities;
namespace web.api.Dtos.Incomming
{
    public class CreateCarDto

    {
        private bool withDriver=false;

        [Required]
        public int Number { get; set; }

        public double EngineCapacity { get; set; }
        [StringLength(15)]
        public string Color { get; set; }

        [StringLength(10)]
        public string Type { get; set; }


        public double DailyFare { get; set; }

        public Guid? DriverId
        {
            get { return DriverId; }
            set
            {
                DriverId = value;
                withDriver = (DriverId != null) ? true : false;
            }
        }


    }
}