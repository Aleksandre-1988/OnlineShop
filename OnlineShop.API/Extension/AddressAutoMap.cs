using OnlineShop.API.Model_Views;
using OnlineShop.Domain.Model;
using AutoMapper;

namespace OnlineShop.API.Extension
{
    public class AddressAutoMap : Profile
    {
        public AddressAutoMap()
        {
            CreateMap<Address, Address_View>()
             .ReverseMap(); //reverse so the both direction
        }
    }
}
