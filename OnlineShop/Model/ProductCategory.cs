using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

        public int NumberOfProducts { get; set; }
    }
}
