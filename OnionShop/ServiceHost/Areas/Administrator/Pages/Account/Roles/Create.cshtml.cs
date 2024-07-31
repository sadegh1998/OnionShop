using AccountManagement.Application.Contract.Role;
using BlogManagement.Application.Contract.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administrator.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        public CreateRole Command;
        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
        }
       
        public JsonResult OnPost(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return new JsonResult(result);

        }
    }
}
