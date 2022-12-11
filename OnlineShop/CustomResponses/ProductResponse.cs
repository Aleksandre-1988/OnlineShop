using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class ProductResponse
    {
        public List<Product> Products {get;set;} = null;
        public Product Product {get;set;} = null;
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
