using _01_ShopQuery.Contracts.ArticleCategory;
using _01_ShopQuery.Contracts.ProductCategory;

namespace _01_ShopQuery
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories{ get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
    }
}
