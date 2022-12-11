using OnlineShop.Domain.Interface;

namespace OnlineShop.DAL.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MainContext _context;

        public IAddressRep addressRep { get; }

        public ICustomerAddressRep customerAddressRep { get; }

        public ICustomerRep customerRep { get; }

        public IProductCategoryRep productCategoryRep { get; }

        public IProductDescriptionRep productDescriptionRep { get; }

        public IProductModelProductDescriptionRep productModelDescriptionRep { get; }

        public IProductModelRep productModelRep { get; }

        public IProductRep productRep { get; }

        public ISalesOrderDetailRep salesOrderDetailRep { get; }

        public ISalesOrderHeaderRep salesOrderHeaderRep { get; }

        public UnitOfWork(MainContext context,
                          IAddressRep addressRep,
                          ICustomerAddressRep customerAddressRep,
                          ICustomerRep customerRep,
                          IProductCategoryRep productCategoryRep,
                          IProductDescriptionRep productDescriptionRep,
                          IProductModelProductDescriptionRep productModelProductDescriptionRep,
                          IProductModelRep productModelRep,
                          IProductRep productRep,
                          ISalesOrderDetailRep salesOrderDetailRep,
                          ISalesOrderHeaderRep salesOrderHeaderRep
                          )
        {
            _context = context;
            this.addressRep = addressRep;
            this.customerAddressRep = customerAddressRep;
            this.customerRep = customerRep;
            this.productCategoryRep = productCategoryRep;
            this.productDescriptionRep = productDescriptionRep;
            this.productModelDescriptionRep = productModelProductDescriptionRep;
            this.productModelRep = productModelRep;
            this.productRep = productRep;
            this.salesOrderDetailRep = salesOrderDetailRep;
            this.salesOrderHeaderRep = salesOrderHeaderRep;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
