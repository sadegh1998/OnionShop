using _0_Framework.Application.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
       private readonly IEmailService _emailService;

        public IndexModel(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void OnGet()
        {
            //_emailService.SendEmail("welcome", "Welcome to shop site", "contact@shop.com");
        }
    }
}
