using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;
using ShopManagement.ApplicationContract.ProductPicture;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administrator.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public List<ProductPictureViewModel> ProductPictures{ get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _product;
        private readonly IProductPictureApplication _productPicture;

        public IndexModel(IProductPictureApplication productPicture, IProductApplication product)
        {
            _productPicture = productPicture;
            _product = product;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_product.GetProducts(), "Id", "Name");
            ProductPictures = _productPicture.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture();
            command.Products = _product.GetProducts();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate( CreateProductPicture command)
        {
           
            var result = _productPicture.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _productPicture.GetDetails(id);
            productPicture.Products = _product.GetProducts();
            return Partial("./Edit", productPicture);
        }
        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPicture.Edit(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetRemove(long id) { 
        var result = _productPicture.Remove(id);

            if (result.IsSuccedded)
               return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id) {
        var result = _productPicture.Restore(id);

            if (result.IsSuccedded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

    }
}
