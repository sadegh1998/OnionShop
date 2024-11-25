using _0_Framework.Application;
using _0_Framework.Application.Email;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Domain.OrderAgg;
using System.Security.Cryptography;
using System.Text;

namespace ServiceHost.Pages
{
    public class ForgetPasswordModel : PageModel
    {

        public string ValueForSendCode { get; set; }
        public AccountViewModel Account;
        private readonly IConfiguration _configuration;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly HmacTokenHelper _hmacTokenHelper;

        public ForgetPasswordModel(IAccountApplication accountApplication, IEmailService emailService, ISmsService smsService, IConfiguration configuration, HmacTokenHelper hmacTokenHelper)
        {
            _accountApplication = accountApplication;
            _emailService = emailService;
            _smsService = smsService;
            _configuration = configuration;
            _hmacTokenHelper = hmacTokenHelper;
        }

        public void OnGet()
        {
        }
        
        public IActionResult OnPost(string ValueForSendCode)
        {

            var result = ValueForSendCode.GetInputType();
            if (result == InputType.Email)
            {
                Account = _accountApplication.GetUserEmailBy(ValueForSendCode);
            }
            else if (result == InputType.Mobile)
            {
                Account = _accountApplication.GetUserMobileBy(ValueForSendCode);
            }
            else
            {
                return Page();
            }
            if (!string.IsNullOrEmpty(Account.Token) && !_hmacTokenHelper.ValidateResetToken(Account.Token, Account.Id.ToString()).IsExpired)
            {
                TempData["ErrorMessage"] = "You can only request a new password reset link after the previous one has expired.";
                return Page();
            }
            //var token = GenerateToken(Account);
            var token = _hmacTokenHelper.GenerateResetToken(Account.Id.ToString());
            var callbackUrl = Url.Page(
           "/ResetPassword",
           pageHandler: null,
           values: new { token, user = ValueForSendCode },
           protocol: Request.Scheme);
            var message = $"برای بازیابی کلمه عبور خود روی لینک ارسال شده کلیک نمایید . در صورتی که شما اقدامی برای فراموشی رمز عبور انجام نداده اید ، روی لینک کلیک نکنید و موارد را سریعا به تیم پشتیبانی اطلاع دهید: <a href='{callbackUrl}'>لینک بازیابی کلمه عبور</a>";

            var setTokenResult = _accountApplication.SetToken(token, Account.Id);
            if (!setTokenResult.IsSuccedded)
            {
                return Page();
            }

            if (result == InputType.Email)
            {
                _emailService.SendEmail("لينك بازيابي كلمه عبور", message, Account.Email);
            }
            else if (result == InputType.Mobile)
            {
                _smsService.Send(Account.Mobile, message);
            }

            return RedirectToPage("/ForgetPasswordConfirmation");
        }

        private string GenerateToken(AccountViewModel user) // Assume User is your user model
        {
            var hmackey = _configuration.GetSection("Hmac");

            // Generate a unique token
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmackey["HmacKey"]));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"{user.Id}:{token}")); // Use user ID

            // Store token with expiration in the database
            var expiration = DateTime.UtcNow.AddHours(1); // Token expires in 1 hour

            return $"{token}:{Convert.ToBase64String(hash)}:{expiration:O}";
        }
    }
}
