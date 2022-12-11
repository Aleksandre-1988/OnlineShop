using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductModelProductDescriptionRep : GenericRep<ProductModelProductDescription>, IProductModelProductDescriptionRep
    {
        public ProductModelProductDescriptionRep(MainContext context) : base(context)
        {
        }
    }
}
