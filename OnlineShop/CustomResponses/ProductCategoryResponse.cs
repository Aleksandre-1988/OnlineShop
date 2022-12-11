using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class ProductCategoryResponse
    {
        public List<ProductCategory> ProductCategories {get;set;} = null;
        public ProductCategory ProductCategory {get;set;} = null;
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
