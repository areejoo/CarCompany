namespace web.core
{
    public class Customer
    {
        public Guid Id{ get; set; }
        public String Name { get; set; }
        public String Phone{ get; set; }
       public String Email{ get; set; }
       public String Address{ get; set; }
       public ICollection<Car> Cars { set; get; }

    }
}
