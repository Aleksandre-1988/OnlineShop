using OnlineShop.Api.Model;
using OnlineShop.Domain.Model;

namespace OnlineShop.API.Extension
{
    public static class Extensions
    {
        public static ProductCategory MapToProductCategory (ProductCategory category, ProdCat_Part prodCat_Part)
        {
            ProductCategory productCategory = new ProductCategory ();

            category.ProductCategoryID= prodCat_Part.ProductCategoryID;
            category.Name = prodCat_Part.Name;
            category.ParentProductCategoryID = prodCat_Part.ParentProductCategoryID;
            category.ModifiedDate = prodCat_Part.ModifiedDate;

            return productCategory;

        }

        public static IEnumerable<ProdCat_Part> ConvertToProductCategoryPart (IEnumerable<ProdCat_Part> prodCat_Parts, IEnumerable<ProductCategory> productCategories)
        {
            return from pc in productCategories
                   select new ProdCat_Part
                   {
                       ProductCategoryID = pc.ProductCategoryID,
                       ParentProductCategoryID = pc.ParentProductCategoryID,
                       ModifiedDate = pc.ModifiedDate,
                       Name = pc.Name,
                       NumberOfProducts = pc.Products.Count()
                   };
        }
    }
}
