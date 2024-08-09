using _01_ShopQuery.Contracts.Order;
using _01_ShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.ApplicationContract.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;

        public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
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
        public IActionResult OnGetPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
            {
                return RedirectToPage("/Cart");
            }

            return RedirectToPage("/CheckOut");
        }
    }
}
