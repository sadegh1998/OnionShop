using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        private readonly IRoleApplication _roleApplication;

        public EditModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
        }
        public IActionResult OnPostEdit(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");

        }
    }
}
