using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Model_Views
{
    public class ProductCategory_View
    {
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

        public int NumberOfProducts { get; set; }

    }
}
