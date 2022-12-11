using OnlineShop.CustomResponses;
using OnlineShop.Model;

namespace OnlineShop.Services.Contracts
{
    public interface IProductService
    {
        Task<ProductResponse> GetAll();
        Task<ProductResponse> Take(int rowCount);

        Task<ProductResponse> Create(Product productToAdd);
        Task<ProductResponse> Remove(int productIdToDelete);
        Task<ProductResponse> Update(Product productToEdit);

        Task<ProductCategoryResponse> GetCategories();

        Task<ProductModelResponse> GetModels();
    }
}
