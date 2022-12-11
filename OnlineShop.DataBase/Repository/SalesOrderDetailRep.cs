using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class SalesOrderDetailRep : GenericRep<SalesOrderDetail>, ISalesOrderDetailRep
    {
        public SalesOrderDetailRep(MainContext context) : base(context)
        {
        }
    }
}
