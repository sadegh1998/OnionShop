using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administrator.Pages.Discount.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts{ get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _product;
        private readonly ICustomerDiscountApplication _customerDiscount;

        public IndexModel(ICustomerDiscountApplication customerDiscount, IProductApplication product)
        {
            _customerDiscount = customerDiscount;
            _product = product;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_product.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscount.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount();
            command.Products = _product.GetProducts();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate( DefineCustomerDiscount command)
        {
           
            var result = _customerDiscount.CreateDiscount(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var discount = _customerDiscount.GetDetails(id);
            discount.Products = _product.GetProducts();
            return Partial("./Edit", discount);
        }
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscount.Edit(command);
            return new JsonResult(result);

        }
       

    }
}
