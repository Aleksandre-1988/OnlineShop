using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class ProductCategoryRep : GenericRep<ProductCategory>, IProductCategoryRep
    {
        protected readonly MainContext Context;

        public ProductCategoryRep(MainContext context) : base(context)
        {
            Context = context;
        }

        
    }
}
