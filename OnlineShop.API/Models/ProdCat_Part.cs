using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Api.Model
{
    public class ProdCat_Part
    {
        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

        public int NumberOfProducts { get; set; }

    }
}
