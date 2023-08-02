
using System.ComponentModel.DataAnnotations;

namespace web.core.Entities
{
    public class Driver:BaseEntity
    {
        
        public String Name { get; set; }
    
        public String Phone{ get; set; }

        public bool IsAvailable{get;set;}

        public Car Car{ get; set; }

        public Driver ReplecmentDriver{ get; set; }






    }
}
