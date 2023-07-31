using System.ComponentModel.DataAnnotations;
      namespace web.core
{
    public class Car:IEntity
    {
        // [Key]
        // public int Id { get; set; }
        public Guid Id { get; set; }
         public string Number{ get; set; }
        public float EngineCapacity{ get; set; }
        public string Color{ get; set; }
        public string Type{ get; set; }
        public float DailyFare{ get; set; }
        public bool WithDriver{ get; set; }
        public int Driver{get; set;}





    }

}
