using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Model_Views
{
    public class Customer_View
    {
        public int CustomerID { get; set; }

        public bool NameStyle { get; set; }

        public string? Title { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Suffix { get; set; }

        public string? CompanyName { get; set; }

        public string? SalesPerson { get; set; }

        public string? EmailAddress { get; set; }

        public string? Phone { get; set; }

        [Unicode(false)]
        public string PasswordHash { get; set; } = null!;

        [Unicode(false)]
        public string PasswordSalt { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
