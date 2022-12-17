using OnlineShop.API.Model_Views;
using OnlineShop.Domain.Model;
using AutoMapper;

namespace OnlineShop.API.Extension
{
    public class ProductCategoryAutoMap : Profile
    {
        public ProductCategoryAutoMap()
        {
            CreateMap<ProductCategory, ProductCategory_View>()
                .ForMember(dest => dest.NumberOfProducts, act => act.MapFrom(src => src.Products.Count()))
                .ReverseMap(); //reverse so the both direction
        }
    }
}
