using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class AddressRep : GenericRep<Address>, IAddressRep
    {
        public AddressRep(MainContext context) : base(context)
        {
        }
    }
}
