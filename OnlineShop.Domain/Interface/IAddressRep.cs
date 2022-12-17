using OnlineShop.Domain.Model;

namespace OnlineShop.Domain.Interface
{
    public interface IAddressRep : IGenericRep<Address>
    {
        List<Address> GetAddressListByCustomerId(int id);
    }
}
