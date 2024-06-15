using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administrator.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public List<ProductViewModel> Products{ get; set; }
        public SelectList ProductCategories { get; set; }
        private readonly IProductApplication _product;
        private readonly IProductCategoryApplication _productCategory;

        public IndexModel(IProductCategoryApplication productCategory, IProductApplication product)
        {
            _productCategory = productCategory;
            _product = product;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategories = new SelectList(_productCategory.GetProductCategories(), "Id", "Name");
            Products = _product.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct();
            command.Categories = _productCategory.GetProductCategories();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate( CreateProduct command)
        {
           
            var result = _product.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var product = _product.Get(id);
            product.Categories = _productCategory.GetProductCategories();
            return Partial("./Edit", product);
        }
        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _product.Edit(command);
            return new JsonResult(result);

        }


    }
}
