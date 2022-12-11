using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductModelRep : GenericRep<ProductModel>, IProductModelRep
    {
        public ProductModelRep(MainContext context) : base(context)
        {
        }
    }
}
