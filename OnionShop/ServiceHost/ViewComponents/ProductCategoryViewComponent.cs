using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategoryViewComponent(IProductCategoryQuery productCategory)
        {
            _productCategory = productCategory;
        }
        public IViewComponentResult Invoke()
        {
            var category = _productCategory.GetProductCategories();
            return View(category);
        }
    }
}
