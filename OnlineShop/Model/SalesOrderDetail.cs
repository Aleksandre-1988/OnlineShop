namespace OnlineShop.Model
{
    public class SalesOrderDetail
    {
        public int SalesOrderID { get; set; }

        public int SalesOrderDetailID { get; set; }

        public short OrderQty { get; set; }

        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        public decimal LineTotal { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
