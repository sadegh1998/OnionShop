using _0_Framework.Infrstructure;
using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData]
        public string LoginMessage { get; set; }
        [TempData]
        public string RegisterMessage { get; set; }
        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPostLogin(Login command)
        {
            var result = _accountApplication.Login(command);
            if (result.IsSuccedded)
            {
                return RedirectToPage("/Index");
            }

            LoginMessage = result.Message;
            return RedirectToPage("/Account");

        }
        public IActionResult OnPostRegister(CreateAccount command)
        {
            command.RoleId = long.Parse(Roles.SiteUser);
            var result = _accountApplication.Create(command);
            if (result.IsSuccedded)
            {
                RegisterMessage = result.Message;
                return RedirectToPage("/Account");
            }
            RegisterMessage = result.Message;
            return RedirectToPage("/Account");
        }
        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");

        }
    }
}
