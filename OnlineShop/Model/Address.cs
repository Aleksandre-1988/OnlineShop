using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class Address
    {
        public int AddressID { get; set; }

        [StringLength(60)]
        [Required(ErrorMessage = "Address Line1 Is Required")]
        public string AddressLine1 { get; set; } = null!;

        [StringLength(60)]
        public string? AddressLine2 { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "City Is Required")]
        public string City { get; set; } = null!;

        [StringLength(50)]
        [Required(ErrorMessage = "State Is Required")]
        public string StateProvince { get; set; } = null!;

        [StringLength(50)]
        [Required(ErrorMessage = "Country Is Required")]
        public string CountryRegion { get; set; } = null!;

        [StringLength(15)]
        [Required(ErrorMessage = "PostalCode Is Required")]
        public string PostalCode { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
