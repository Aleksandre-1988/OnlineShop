using OnlineShop.Api.Model;
using OnlineShop.Domain.Model;

namespace OnlineShop.API.Extension
{
    public static class Extensions
    {
        public static ProductCategory MapToProductCategory (ProductCategory category, ProductCategory_View prodCat_Part)
        {
            ProductCategory productCategory = new ProductCategory ();

            category.ProductCategoryID= prodCat_Part.ProductCategoryID;
            category.Name = prodCat_Part.Name;
            category.ParentProductCategoryID = prodCat_Part.ParentProductCategoryID;
            category.ModifiedDate = prodCat_Part.ModifiedDate;

            return productCategory;

        }

        public static List<ProductCategory_View> ConvertToProductCategoryPart (this List<ProductCategory_View> prodCat_ViewList, List<ProductCategory> productCategories)
        {
            foreach (var item in productCategories)
            {
                ProductCategory_View productCategory_View = new ProductCategory_View();

                productCategory_View.ProductCategoryID = item.ProductCategoryID;
                productCategory_View.ParentProductCategoryID = item.ParentProductCategoryID;
                productCategory_View.Name = item.Name;
                productCategory_View.ModifiedDate = item.ModifiedDate;
                productCategory_View.NumberOfProducts = item.Products.Count();

                prodCat_ViewList.Add(productCategory_View);
            }
            return prodCat_ViewList;
        }
    }
}
