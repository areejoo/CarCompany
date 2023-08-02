
using System.ComponentModel.DataAnnotations;


namespace web.core.Entities
{
    public class Customer:BaseEntity
    {
        
        public String Name { get; set; }
        
        public String Phone{ get; set; }

        public String Email{ get; set; }
       
        public String Address{ get; set; }
       
        public ICollection<Car> Cars{ set; get; }

    }
}
