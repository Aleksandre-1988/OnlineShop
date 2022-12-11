using OnlineShop.Model;

namespace OnlineShop.CustomResponses
{
    public class ProductModelResponse
    {
        public List<ProductModel> ProductModels {get;set;} = null;
        public ProductModel ProductModel {get;set;} = null;
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
