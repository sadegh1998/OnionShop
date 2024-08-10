using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentResultModel : PageModel
    {
        public PaymentResult PaymentResult;
        public void OnGet(PaymentResult paymentResult)
        {
            PaymentResult = paymentResult;
        }
    }
}
