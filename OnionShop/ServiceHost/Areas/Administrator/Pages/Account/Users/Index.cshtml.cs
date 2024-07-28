using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
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
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }
        
        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount();
            command.Roles = _roleApplication.List();
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
            account.Roles = _roleApplication.List();
            return Partial("./Edit", account);
        }
        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);

        }
        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChanagePassword { Id = id};
            return Partial("./ChangePassword",command);
        }
        public JsonResult OnPostChangePassword(ChanagePassword commad)
        {
            var result = _accountApplication.ChangePassword(commad);
            return new JsonResult(result);

        }

    }
}
