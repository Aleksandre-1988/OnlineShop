using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class ProductCategoryResponse
    {
        public List<ProductCategory>? ProductCategories {get;set;}
        public ProductCategory? ProductCategory {get;set;}
        public bool Status { get; set; }
        public string? Message { get; set; }
    }
}
