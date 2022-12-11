using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class SalesOrderHeader
    {
        public int SalesOrderID { get; set; }

        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public byte Status { get; set; }

        [Required]
        public bool? OnlineOrderFlag { get; set; }

        [StringLength(25)]
        public string SalesOrderNumber { get; set; } = null!;

        [StringLength(25)]
        public string? PurchaseOrderNumber { get; set; }

        [StringLength(15)]
        public string? AccountNumber { get; set; }

        public int CustomerID { get; set; }

        public int? ShipToAddressID { get; set; }

        public int? BillToAddressID { get; set; }

        [StringLength(50)]
        public string ShipMethod { get; set; } = null!;

        [StringLength(15)]
        public string? CreditCardApprovalCode { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxAmt { get; set; }

        public decimal Freight { get; set; }

        public decimal TotalDue { get; set; }

        public string? Comment { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
