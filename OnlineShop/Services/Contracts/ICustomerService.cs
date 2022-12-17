using OnlineShop.CustomResponses;
using OnlineShop.Model;

namespace OnlineShop.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerResponse> GetById(int id);
        Task<CustomerResponse> GetAll();
        Task<CustomerResponse> Take(int rowCount);

        Task<CustomerResponse> Create(Customer customerToAdd);
        Task<CustomerResponse> Remove(int customerIdToDelete);
        Task<CustomerResponse> Update(Customer customerToEdit);
    }
}
