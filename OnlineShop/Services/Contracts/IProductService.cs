using OnlineShop.CustomResponses;
using OnlineShop.Model;

namespace OnlineShop.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductResponse> GetById(int id);
        Task<ProductResponse> Take(int maxId);
        Task<ProductResponse> GetAll();

        Task<ProductResponse> Create(Product productToAdd);
        Task<ProductResponse> Remove(int productIdToDelete);
        Task<ProductResponse> Update(Product productToEdit);

        Task<ProductCategoryResponse> GetCategories();

        Task<ProductModelResponse> GetModels();
    }
}
