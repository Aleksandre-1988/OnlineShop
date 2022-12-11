using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class ProductDescription
    {
        public int ProductDescriptionID { get; set; }

        [StringLength(400)]
        public string Description { get; set; } = null!;

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
