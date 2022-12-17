using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Model
{
    public class CustomerAddress
    {
        public int CustomerID { get; set; }

        public int AddressID { get; set; }

        [StringLength(50)]
        [Required]
        public string AddressType { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
