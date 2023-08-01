using System.ComponentModel.DataAnnotations;
using web.core.Data;

namespace web.core.Models
{
    public class Car:IEntity
    {
        public Guid Id { get; set; }
        
        public int Number{ get; set; }

        public float EngineCapacity{ get; set; }
        
      
        public string Color{ get; set; }

        
        public string Type{ get; set; }

        
        public double DailyFare{ get; set; }
        
    
        public bool WithDriver{ get; set; }

      
        public Driver Driver{get; set;}





    }

}
