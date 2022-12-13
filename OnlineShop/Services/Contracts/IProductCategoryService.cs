using OnlineShop.CustomResponses;

namespace OnlineShop.Services.Contracts
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryResponse> GetProductCategories();
    }
}
