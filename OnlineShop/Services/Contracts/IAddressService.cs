using OnlineShop.CustomResponses;
using OnlineShop.Model;

namespace OnlineShop.Services.Contracts
{
    public interface IAddressService
    {
        Task<AddressResponse> GetByCustomerId(int id);
        Task<AddressResponse> GetAll();

        Task<AddressResponse> Create(Address addressToAdd);
        Task<AddressResponse> Remove(int addressIdToDelete);
        Task<AddressResponse> Update(Address addressToEdit);
    }
}
