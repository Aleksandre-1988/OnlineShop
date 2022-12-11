using Microsoft.Extensions.DependencyInjection;
using OnlineShop.DAL.Repository;
using OnlineShop.Domain.Interface;

namespace OnlineShop.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IAddressRep, AddressRep>();

            services.AddTransient<IProductRep, ProductRep>();

            services.AddTransient<ICustomerRep, CustomerRep>();

            services.AddTransient<ISalesOrderHeaderRep, SalesOrderHeaderRep>();

            //Dont Needed in project Description
            services.AddTransient<ICustomerAddressRep, CustomerAddressRep>();
            services.AddTransient<IProductCategoryRep, ProductCategoryRep>();
            services.AddTransient<IProductDescriptionRep, ProductDescriptionRep>();
            services.AddTransient<IProductModelProductDescriptionRep, ProductModelProductDescriptionRep>();
            services.AddTransient<IProductModelRep, ProductModelRep>();
            services.AddTransient<ISalesOrderDetailRep, SalesOrderDetailRep>();

            return services;
        }
    }
}