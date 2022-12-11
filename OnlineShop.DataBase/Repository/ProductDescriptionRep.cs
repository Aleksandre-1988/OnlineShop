using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductDescriptionRep : GenericRep<ProductDescription>, IProductDescriptionRep
    {
        public ProductDescriptionRep(MainContext context) : base(context)
        {
        }
    }
}
