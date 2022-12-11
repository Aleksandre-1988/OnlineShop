﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Domain.Model
{
    /// <summary>
    /// Street address information for customers.
    /// </summary>
    [Table("Address", Schema = "SalesLT")]
    [Index("rowguid", Name = "AK_Address_rowguid", IsUnique = true)]
    [Index("AddressLine1", "AddressLine2", "City", "StateProvince", "PostalCode", "CountryRegion", Name = "IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion")]
    [Index("StateProvince", Name = "IX_Address_StateProvince")]
    public class Address
    {
        public Address()
        {
            CustomerAddresses = new HashSet<CustomerAddress>();
            SalesOrderHeaderBillToAddresses = new HashSet<SalesOrderHeader>();
            SalesOrderHeaderShipToAddresses = new HashSet<SalesOrderHeader>();
        }

        /// <summary>
        /// Primary key for Address records.
        /// </summary>
        [Key]
        public int AddressID { get; set; }
        /// <summary>
        /// First street address line.
        /// </summary>
        [StringLength(60)]
        public string AddressLine1 { get; set; } = null!;
        /// <summary>
        /// Second street address line.
        /// </summary>
        [StringLength(60)]
        public string? AddressLine2 { get; set; }
        /// <summary>
        /// Name of the city.
        /// </summary>
        [StringLength(30)]
        public string City { get; set; } = null!;
        /// <summary>
        /// Name of state or province.
        /// </summary>
        [StringLength(50)]
        public string StateProvince { get; set; } = null!;
        [StringLength(50)]
        public string CountryRegion { get; set; } = null!;
        /// <summary>
        /// Postal code for the street address.
        /// </summary>
        [StringLength(15)]
        public string PostalCode { get; set; } = null!;
        /// <summary>
        /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        public Guid rowguid { get; set; }
        /// <summary>
        /// Date and time the record was last updated.
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("Address")]
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        [InverseProperty("BillToAddress")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddresses { get; set; }
        [InverseProperty("ShipToAddress")]
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddresses { get; set; }
    }
}
