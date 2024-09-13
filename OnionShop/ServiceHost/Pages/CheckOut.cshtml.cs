using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Application.Zipal;
using _01_ShopQuery.Contracts.Order;
using _01_ShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application;
using ShopManagement.ApplicationContract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IAuthHelper _authHelper;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IZipalFactory _zipaFactory;
        public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IAuthHelper authHelper, IZarinPalFactory zarinPalFactory, IZipalFactory zipaFactory)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _authHelper = authHelper;
            _zarinPalFactory = zarinPalFactory;
            Cart = new Cart();
            _zipaFactory = zipaFactory;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);

            foreach (var item in cartItems)
            {
                item.CalculateTotalItemAmount();
            }
            Cart = _cartCalculatorService.CamputeCart(cartItems);
            _cartService.Set(Cart);
        }
        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);   
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }

            var orderId = _orderApplication.PlaceOrder(cart);
            if(paymentMethod == 1)
            {
                var paymentResponse = _zipaFactory.CreatePaymentRequest(cart.PayAmount.ToString(), "خرید از فروشگاه", orderId.ToString(),"","","");
                return Redirect($"{_zipaFactory.Prefix}/start/{paymentResponse.trackId}");
            //var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(),"","","خرید از فروشگاه",orderId);
            //return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }
            else
            {
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش با موفقیت ثبت گردید و پس از تماس کارشناسان و واریز وجه آماده ارسال می باشد.", ""));
            }
        }
        public IActionResult OnGetCallBack([FromQuery] long success, [FromQuery] long status, [FromQuery] long orderId , [FromQuery] long trackId)
        {
            //var orderAmount = _orderApplication.GetAmountBy(orderId);
            var verificationResponse = _zipaFactory.CreateVerificationRequest(trackId);
            var paymentResult = new PaymentResult();

            if ( verificationResponse.status == 1)
            {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(orderId, verificationResponse.status);
                Response.Cookies.Delete(CookieName);
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("تراکنش با موفقیت انجام شد", issueTrackingNo));
            }
            else
            {
                paymentResult = paymentResult.Failed("پرداخت با موفقیت انجام نشد . در صورت کسر وجه از حساب ، تا 72 ساعت آینده به حساب شما بازخواهد گشت");
                return RedirectToPage("/PaymentResult", paymentResult);
            }
        }

        //public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] long oId)
        //{
        //    var orderAmount = _orderApplication.GetAmountBy(oId);
        //    var bb = _zipaFactory.CreateVerificationRequest(trackid);
        //    var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());
        //    var paymentResult = new PaymentResult();

        //    if (status == "OK" && verificationResponse.RefID >= 100)
        //    {
        //       var issueTrackingNo =  _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
        //        Response.Cookies.Delete(CookieName);
        //        return RedirectToPage("/PaymentResult", paymentResult.Succeeded("تراکنش با موفقیت انجام شد", issueTrackingNo));
        //    }
        //    else
        //    {
        //        paymentResult = paymentResult.Failed("پرداخت با موفقیت انجام نشد . در صورت کسر وجه از حساب ، تا 72 ساعت آینده به حساب شما بازخواهد گشت");
        //        return RedirectToPage("/PaymentResult",paymentResult);
        //    }
        //}
    }
}
