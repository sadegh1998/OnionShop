using _0_Framework.Infrstructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administrator.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        public List<SelectListItem> Permissions = new List<SelectListItem>();
        private IEnumerable<IPermissionExposer> _exposer;
        private readonly IRoleApplication _roleApplication;

        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposer)
        {
            _roleApplication = roleApplication;
            _exposer = exposer;
        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
            foreach(var expose in _exposer)
            {
                var rolePermissions = expose.Expose();
                foreach(var (key , value) in rolePermissions)
                {
                    var group = new SelectListGroup { 
                    Name  = key,
                    };
                    foreach(var permission in value)
                    {
                        var item = new SelectListItem(permission.Name,permission.Code.ToString()) 
                        {
                            Group = group,
                        };
                        if(Command.MappedPermissions.Any(x=>x.Code == permission.Code))
                        {
                            item.Selected = true;
                        }

                        Permissions.Add(item);
                    }
                }
            }
        }
        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");

        }
    }
}
