using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class CustomerRep : GenericRep<Customer>, ICustomerRep
    {
        public CustomerRep(MainContext context) : base(context)
        {
        }
    }
}
