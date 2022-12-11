using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class ProductModelProductDescription
    {
        public int ProductModelID { get; set; }

        public int ProductDescriptionID { get; set; }

        [StringLength(6)]
        public string Culture { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
