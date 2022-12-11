using OnlineShop.Domain.Interface;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL.Repository
{
    internal class SalesOrderHeaderRep : GenericRep<SalesOrderHeader>, ISalesOrderHeaderRep
    {
        public SalesOrderHeaderRep(MainContext context) : base(context)
        {
        }
    }
}
