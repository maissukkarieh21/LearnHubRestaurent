namespace LearnHubRestaurent.Models
{
    public class Order
    {
        public Customerr Customerr { get; set; }
        public ProductCustomerr ProductCustomerr { get; set; }  
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
