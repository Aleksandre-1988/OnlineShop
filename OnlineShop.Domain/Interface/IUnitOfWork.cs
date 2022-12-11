namespace OnlineShop.Domain.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRep addressRep { get; }
        ICustomerAddressRep customerAddressRep { get; }
        ICustomerRep customerRep { get; }
        IProductCategoryRep productCategoryRep { get; }
        IProductDescriptionRep productDescriptionRep { get; }
        IProductModelProductDescriptionRep productModelDescriptionRep { get; }
        IProductModelRep productModelRep { get; }
        IProductRep productRep { get; }
        ISalesOrderDetailRep salesOrderDetailRep { get; }
        ISalesOrderHeaderRep salesOrderHeaderRep { get; }

        int Save();
    }
}
