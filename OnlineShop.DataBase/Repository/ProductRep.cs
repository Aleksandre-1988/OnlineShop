using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductRep : GenericRep<Product>, IProductRep
    {
        public ProductRep(MainContext context) : base(context)
        {
        }
    }
}
