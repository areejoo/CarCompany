using web.core.Data;

using System.ComponentModel.DataAnnotations;


namespace web.core.Models
{
    public class Customer:IEntity
    {
        public Guid Id{ get; set; }
        
        public String Name { get; set; }
        
        public String Phone{ get; set; }

        public String Email{ get; set; }
       
        public String Address{ get; set; }
       
        public ICollection<Car> Cars{ set; get; }

    }
}
