using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class AddressRep : GenericRep<Address>, IAddressRep
    {
        private readonly MainContext _context;
        public AddressRep(MainContext context) : base(context)
        {
            _context = context;
        }

        public List<Address> GetAddressListByCustomerId(int id)
        {
            List<int> addressIds = _context.CustomerAddresses.Where(x=> x.CustomerID == id).Select(x => x.AddressID).ToList();
            var addressList = _context.Addresses.Where(x => addressIds.Contains(x.AddressID)).ToList();

            return addressList; 
        }
    }
}
