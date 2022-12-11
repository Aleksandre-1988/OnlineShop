using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public bool NameStyle { get; set; }

        [StringLength(8)]
        public string? Title { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [StringLength(50)]
        public string? MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(10)]
        public string? Suffix { get; set; }

        [StringLength(128)]
        public string? CompanyName { get; set; }

        [StringLength(256)]
        public string? SalesPerson { get; set; }

        [StringLength(50)]
        public string? EmailAddress { get; set; }

        [StringLength(25)]
        public string? Phone { get; set; }

        [StringLength(128)]
        public string PasswordHash { get; set; } = null!;

        [StringLength(10)]
        public string PasswordSalt { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
