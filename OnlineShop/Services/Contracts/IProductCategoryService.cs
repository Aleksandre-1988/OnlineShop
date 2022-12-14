using OnlineShop.CustomResponses;
using OnlineShop.Model;

namespace OnlineShop.Services.Contracts
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryResponse> GetProductCategories();
        Task<ProductCategoryResponse> Add(ProductCategory productCategoryToAdd);
        Task<ProductCategoryResponse> Edit(ProductCategory productCategoryToEdit);
        Task<ProductCategoryResponse> Delete(int id);

    }
}
