using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        public ChanagePassword ChanagePassword { get; set; } = new ChanagePassword();
        public AccountViewModel Account { get; set; }


        public ResetPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet(string token,string user)
        {
            Account = _accountApplication.GetUserEmailBy(user);

            ChanagePassword.Id = Account.Id;
        }
        public IActionResult OnPost(ChanagePassword chanagePassword)
        {
               
                var result = _accountApplication.ChangePassword(chanagePassword);
                return new JsonResult(result);
            
        }
    }
}
