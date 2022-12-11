using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class CustomerAddressRep : GenericRep<CustomerAddress>, ICustomerAddressRep
    {
        public CustomerAddressRep(MainContext context) : base(context)
        {
        }
    }
}
