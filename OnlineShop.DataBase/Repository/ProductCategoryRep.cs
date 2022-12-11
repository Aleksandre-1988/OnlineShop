using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductCategoryRep : GenericRep<ProductCategory>, IProductCategoryRep
    {
        public ProductCategoryRep(MainContext context) : base(context)
        {
        }
    }
}
