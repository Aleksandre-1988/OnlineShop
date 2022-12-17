using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Model_Views
{
    public class Address_View
    {
        public int AddressID { get; set; }

        [StringLength(60)]
        public string AddressLine1 { get; set; } = null!;

        [StringLength(60)]
        public string? AddressLine2 { get; set; }

        [StringLength(30)]
        public string City { get; set; } = null!;

        [StringLength(50)]
        public string StateProvince { get; set; } = null!;

        [StringLength(50)]
        public string CountryRegion { get; set; } = null!;

        [StringLength(15)]
        public string PostalCode { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
