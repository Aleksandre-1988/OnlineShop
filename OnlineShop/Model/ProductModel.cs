using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class ProductModel
    {
        public int ProductModelID { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public string? CatalogDescription { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
