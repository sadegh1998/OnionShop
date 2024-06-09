using DiscountManagement.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ServiceHost.Areas.Administrator.Pages.Discount.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public SelectList Products { get; set; }
        private readonly IProductApplication _product;
        private readonly IColleagueDiscountApplication _colleagueDiscount;

        public IndexModel(IColleagueDiscountApplication colleagueDiscount, IProductApplication product)
        {
            _colleagueDiscount = colleagueDiscount;
            _product = product;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_product.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscount.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount();
            command.Products = _product.GetProducts();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
           
            var result = _colleagueDiscount.Define(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var discount = _colleagueDiscount.GetDetails(id);
            discount.Products = _product.GetProducts();
            return Partial("./Edit", discount);
        }
        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscount.Edit(command);
            return new JsonResult(result);

        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscount.Restore(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");

        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscount.Remove(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");

        }


    }
}
