using _0_Framework.Infrstructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.ApplicationContract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel { get; set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        private readonly IProductCategoryApplication _productCategory;

        public IndexModel(IProductCategoryApplication productCategory)
        {
            _productCategory = productCategory;
        }
        [NeedsPermission(ShopPermission.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategory.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        [NeedsPermission(ShopPermission.CreateProductCategory)]
        public JsonResult OnPostCreate( CreateProductCategory command)
        {
            var result = _productCategory.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategory.Get(id);
            return Partial("./Edit", productCategory);
        }
        [NeedsPermission(ShopPermission.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategory.Edit(command);
            return new JsonResult(result);

        }
    }
}
