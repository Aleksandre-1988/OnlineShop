using AutoMapper;
using OnlineShop.Api.Model_Views;
using OnlineShop.Domain.Model;

namespace OnlineShop.API.Extension
{
    public class ProductAutoMap : Profile
    {
        public ProductAutoMap()
        {
            CreateMap<Product, Product_View>().ReverseMap(); //reverse so the both direction
        }
    }
}
