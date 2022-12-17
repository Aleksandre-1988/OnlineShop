using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model
{
    public class Product
    {
        public int ProductID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; } = null!;

        [StringLength(25)]
        [Required(ErrorMessage = "Product Number is required")]
        public string ProductNumber { get; set; } = null!;

        [StringLength(15)]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Standard Cost is required")]
        public decimal StandardCost { get; set; }

        [Required(ErrorMessage = "ListPrice is required")]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        public decimal? Weight { get; set; }

        public int? ProductCategoryID { get; set; }

        public int? ProductModelID { get; set; }

        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public byte[]? ThumbNailPhoto { get; set; }

        [StringLength(50)]
        public string? ThumbnailPhotoFileName { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int NumberOfOrders { get; set; }

        public virtual IEnumerable<ProductCategory> ProductCategories { get; set; }

    }
}
