using web.core.Data;

using System.ComponentModel.DataAnnotations;

namespace web.core.Models
{
    public class Driver:IEntity
    {
        public Guid Id{ get; set; }
        
        public String Name { get; set; }
    
        public String Phone{ get; set; }

      
        public bool IsAvailable{get;set;}

        public Car Car{ get; set; }

        public Driver ReplecmentDriver{ get; set; }






    }
}
