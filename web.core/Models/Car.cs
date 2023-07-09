using System.ComponentModel.DataAnnotations;
      namespace web.core
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public float EngineCapacity{ get; set; }
        public String Color{ get; set; }
        public String Type{ get; set; }
        public String DailyFare{ get; set; }
        public bool WithDriver{ get; set; }
        public ICollection<Driver>? Drivers{get;set;}





    }

}
