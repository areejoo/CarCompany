using System.ComponentModel.DataAnnotations;

namespace web.core.Entities
{
    public class Car:BaseEntity
    {
        
        public int Number{ get; set; }

        public float EngineCapacity{ get; set; }
        
        public string Color{ get; set; }

        
        public string Type{ get; set; }

        
        public double DailyFare{ get; set; }
        
    
        public bool WithDriver{ get; set; }

        public Driver Driver{get; set;}
        
        public Customer Customer{get; set;}





    }

}
