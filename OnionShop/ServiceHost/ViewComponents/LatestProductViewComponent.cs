using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using _01_ShopQuery.Query;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestProductViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestProductViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = _productQuery.GetLatestProducts();
            return View(result);
        }
    }
}
