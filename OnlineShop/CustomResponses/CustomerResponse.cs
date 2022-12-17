using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class CustomerResponse
    {
        public List<Customer?> Customers { get; set; } 
        public Customer? Customer { get; set; } 
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}