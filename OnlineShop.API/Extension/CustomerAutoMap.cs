using OnlineShop.Domain.Model;
using AutoMapper;
using OnlineShop.API.Model_Views;

namespace OnlineShop.API.Extension
{
    public class CustomerAutoMap : Profile
    {
        public CustomerAutoMap()
        {
            CreateMap<Customer, Customer_View>()
             .ReverseMap(); //reverse so the both direction
        }
    }
}
