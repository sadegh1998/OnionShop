using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.ApplicationContract.Product;
using ShopManagement.ApplicationContract.ProductCategory;

namespace ServiceHost.Areas.Administrator.Pages.Account.Users
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public List<AccountViewModel> Accounts{ get; set; }
        public SelectList Roles { get; set; }
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            //ProductCategories = new SelectList(_accountApplicationCategory.GetProductCategories(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount();
            //command.Categories = _accountApplicationCategory.GetProductCategories();
            return Partial("./Create", command);
        }
        public JsonResult OnPostCreate( CreateAccount command)
        {
           
            var result = _accountApplication.Create(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            //product.Categories = _accountApplicationCategory.GetProductCategories();
            return Partial("./Edit", account);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);

        }


    }
}
