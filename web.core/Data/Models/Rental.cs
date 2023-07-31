namespace web.core
{
    public class Rental
    {
        public Guid Id{ get; set; }
        public Customer Customer { get; set; }
        public Car car{ get; set; }
        public DateTime created_at{ get; set; }
        public int numOfDays{ get; set; }
     

    }
}
