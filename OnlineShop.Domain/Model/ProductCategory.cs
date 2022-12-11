using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Domain.Model
{
    /// <summary>
    /// High-level product categorization.
    /// </summary>
    [Table("ProductCategory", Schema = "SalesLT")]
    [Index("Name", Name = "AK_ProductCategory_Name", IsUnique = true)]
    [Index("rowguid", Name = "AK_ProductCategory_rowguid", IsUnique = true)]
    public class ProductCategory
    {
        public ProductCategory()
        {
            InverseParentProductCategory = new HashSet<ProductCategory>();
            Products = new HashSet<Product>();
        }

        /// <summary>
        /// Primary key for ProductCategory records.
        /// </summary>
        [Key]
        public int ProductCategoryID { get; set; }
        /// <summary>
        /// Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.
        /// </summary>
        public int? ParentProductCategoryID { get; set; }
        /// <summary>
        /// Category description.
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; } = null!;
        /// <summary>
        /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        /// </summary>
        public Guid rowguid { get; set; }
        /// <summary>
        /// Date and time the record was last updated.
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("ParentProductCategoryID")]
        [InverseProperty("InverseParentProductCategory")]
        public virtual ProductCategory? ParentProductCategory { get; set; }
        [InverseProperty("ParentProductCategory")]
        public virtual ICollection<ProductCategory> InverseParentProductCategory { get; set; }
        [InverseProperty("ProductCategory")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
